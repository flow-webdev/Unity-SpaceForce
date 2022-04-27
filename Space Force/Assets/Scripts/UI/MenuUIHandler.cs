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
    public Canvas home;
    public Canvas quit;
    public Canvas settings;
    public Canvas pause;
    public Canvas gameOver;

    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private bool fadeIn = true;

    [SerializeField] private LevelManager levelManager;

    [SerializeField]  private bool oppositeBool = false;
    [SerializeField]  private bool mainMenuBool = false;
    public bool isPause = false;

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
    }

    public void StartNew() {
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
        home.gameObject.SetActive(false);
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

    public void OnGameOver() {
        
        gameOver.gameObject.SetActive(true);

        if (fadeIn) {
            if (gameOverCanvasGroup.alpha < 1) {
                gameOverCanvasGroup.alpha += Time.deltaTime;
            } else if (gameOverCanvasGroup.alpha >= 1) {
                fadeIn = false;
            }
        }
    }

    public void OnMainMenu() {
        if (GameManager.Instance.isGameOver) { //&& mainMenuBool
            SceneManager.LoadScene(0);
            fadeIn = true;            
            gameOverCanvasGroup.alpha = 0;            
            gameOver.gameObject.SetActive(false);
            home.gameObject.SetActive(true);            
        }
        GameManager.Instance.ResetNewGame();
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
