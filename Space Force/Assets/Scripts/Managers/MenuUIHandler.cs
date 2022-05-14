using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{

    // In button -> Navigation, put None. Otherwise the spacebar will trigger the last canvas used before the actual one

    public static MenuUIHandler Instance;
    public Canvas home; // Utilized by GameManager to set it inactive
    [SerializeField] private Canvas quit;
    [SerializeField] private Canvas settings;
    [SerializeField] private Canvas pause;
    [SerializeField] private Canvas gameOver;
    [SerializeField] private Canvas victory;
    [SerializeField] private Canvas bombText;
    [SerializeField] private Canvas livesText;
    [SerializeField] private Canvas pointsText;
    [SerializeField] private Canvas timeText;
    [SerializeField] private GameObject pointsVictory;
    [SerializeField] private GameObject lastVictoryButton;
    [SerializeField] private GameObject victoryBadge;

    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private CanvasGroup victoryCanvasGroup;
    [SerializeField] private bool fadeIn = true;

    [SerializeField] private LevelManager levelManager;

    [SerializeField]  private bool oppositeBool = false;
    private string sceneName;
    public bool isPause = false;

    public bool isMenu = false; // These bools help to control the AudioManager
    public bool isLevel = false;
    public bool isWin = false;
    public bool isLose = false;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // This code enables you to access the MenuUI object from any other script. 
    }

    private void Start() {
        gameOverCanvasGroup = gameOver.GetComponent<CanvasGroup>();
        victoryCanvasGroup = victory.GetComponent<CanvasGroup>();        
    }

    public void StartNew() {
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine() { // Start the game. Called by menu
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
        GameManager.Instance.StartLevel(); // Reset timer Coroutine and isVictory var
        bombText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        pointsText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        isMenu = false;
        isLevel = true;
        isWin = false;
        isLose = false;
    }

    public void OnQuit() { // Called by menu
        oppositeBool = !oppositeBool;
        quit.gameObject.SetActive(oppositeBool);
    }

    public void OnSettings() { // Called by menu
        oppositeBool = !oppositeBool;
        settings.gameObject.SetActive(oppositeBool);
    }

    public void OnPause() { // Called by menu
        isPause = true;
        pause.gameObject.SetActive(isPause);
    }

    public void OnResume() { // Called by menu
        isPause = false;
        pause.gameObject.SetActive(isPause);
        levelManager.PauseGame();
    }

    public void OnGameOver() { // Called by LevelManager if lives = 0
        gameOver.gameObject.SetActive(true);
        isMenu = false;
        isLevel = false;
        isWin = false;
        isLose = true;

        if (fadeIn) {
            if (gameOverCanvasGroup.alpha < 1) {
                gameOverCanvasGroup.alpha += Time.deltaTime;
            } else if (gameOverCanvasGroup.alpha >= 1) {
                fadeIn = false;
            }
        }
    }

    public void OnVictory() { // Called by LevelManager if GameManager.Instance.isVictory = true
        victory.gameObject.SetActive(true);
        isMenu = false;
        isLevel = false;
        isWin = true;
        isLose = false;

        if (fadeIn) {
            if (victoryCanvasGroup.alpha < 1) {
                victoryCanvasGroup.alpha += Time.deltaTime;
            } else if (victoryCanvasGroup.alpha >= 1) {
                fadeIn = false;
            }
        }
        StartCoroutine(ShowOnPointsVictory());
    }

    private IEnumerator ShowOnPointsVictory() { // Show points canvas on victory
        yield return new WaitForSeconds(3f);
        pointsVictory.gameObject.SetActive(true);

        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName != "Level 3") {
            StartCoroutine(StartNextLevelCoroutine());
        } else {
            StartCoroutine(VictoryButtonRoutine());
        }
        
    }

    private IEnumerator VictoryButtonRoutine() {
        yield return new WaitForSeconds(2f);
        lastVictoryButton.gameObject.SetActive(true);
        victoryBadge.gameObject.SetActive(true);
    }

    private IEnumerator StartNextLevelCoroutine() { // Start new Level
        yield return new WaitForSeconds(3f);
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        switch (sceneName) {
            case "Level 1":
                SceneManager.LoadScene(2);
                NextLevel();
                StopAllCoroutines();
                break;
            case "Level 2":
                SceneManager.LoadScene(3);
                NextLevel();
                StopAllCoroutines();
                break;
        }
    }

    private void NextLevel() {
        victory.gameObject.SetActive(false);
        GameManager.Instance.StartLevel(); // Reset timer Coroutine and isVictory var
        resetFadeIn();
        isMenu = false;
        isLevel = true;
        isWin = false;
        isLose = false;
    }

    public void OnMainMenu() { // Called by button in GameOver canvas
        
        SceneManager.LoadScene(0);
        resetFadeIn();
        gameOver.gameObject.SetActive(false);
        lastVictoryButton.gameObject.SetActive(false);
        victoryBadge.gameObject.SetActive(false);
        victory.gameObject.SetActive(false);
        home.gameObject.SetActive(true);

        GameManager.Instance.ResetNewGame();
        bombText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        pointsText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        isMenu = true;
        isLevel = false;
        isWin = false;
        isLose = false;
    }

    private void resetFadeIn() {
        fadeIn = true;
        gameOverCanvasGroup.alpha = 0;
        pointsVictory.gameObject.SetActive(false);
    }

    public void OnExitGame() {
        // CONDITIONAL COMPILING: # lines are instruction for the compiler, the false part will be removed
        //      when code is compiled in the editor, first method, otherwise second method
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
