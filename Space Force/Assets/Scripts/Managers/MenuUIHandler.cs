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
    [SerializeField] private GameObject pointsVictory;

    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private CanvasGroup victoryCanvasGroup;
    [SerializeField] private bool fadeIn = true;

    [SerializeField] private LevelManager levelManager;

    [SerializeField]  private bool oppositeBool = false;
    public bool isPause = false;

    public bool isMenu = false;
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

    private IEnumerator StartNewGameCoroutine() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
        isMenu = false;
        isLevel = true;
        isWin = false;
        isLose = false;
    }

    public void OnQuit() {
        oppositeBool = !oppositeBool;
        quit.gameObject.SetActive(oppositeBool);
    }

    public void OnSettings() {
        oppositeBool = !oppositeBool;
        settings.gameObject.SetActive(oppositeBool);
    }

    public void OnPause() {
        isPause = true;
        pause.gameObject.SetActive(isPause);
    }

    public void OnResume() {
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

    private IEnumerator ShowOnPointsVictory() {
        yield return new WaitForSeconds(3f);
        pointsVictory.gameObject.SetActive(true);

        StartCoroutine(StartNextLevelCoroutine());
    }

    private IEnumerator StartNextLevelCoroutine() {
        yield return new WaitForSeconds(3f);
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level 1") {
            SceneManager.LoadScene(2);            
        }
        victory.gameObject.SetActive(false);
        GameManager.Instance.isVictory = false;
        isMenu = false;
        isLevel = true;
        isWin = false;
        isLose = false;
    }

    public void OnMainMenu() { // Called by button in GameOver canvas
        if (GameManager.Instance.isGameOver) {
            SceneManager.LoadScene(0);
            fadeIn = true;            
            gameOverCanvasGroup.alpha = 0;            
            gameOver.gameObject.SetActive(false);
            home.gameObject.SetActive(true);            
        }
        GameManager.Instance.ResetNewGame();
        isMenu = true;
        isLevel = false;
        isWin = false;
        isLose = false;
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
