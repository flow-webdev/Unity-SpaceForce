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

    public LevelManager levelManager;

    private bool oppositeBool = false;
    public bool isPause = false;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // This code enables you to access the MainManager object from any other script. 
    }

    public void StartNew() {
        StartCoroutine(StartNewCoroutine());
    }

    public IEnumerator StartNewCoroutine() {
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
