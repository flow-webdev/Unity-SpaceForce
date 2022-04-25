using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public Canvas home;
    public Canvas quit;
    public Canvas settings;

    private bool oppositeBool = false;

    public void StartNew() {
        SceneManager.LoadScene(1);
    }

    public void OnSettings() {
        oppositeBool = !oppositeBool;
        settings.gameObject.SetActive(oppositeBool);
    }

    public void OnQuit() {
        oppositeBool = !oppositeBool;
        quit.gameObject.SetActive(oppositeBool);
    }

    public void OnExit() {
        // CONDITIONAL COMPILING: # lines are instruction for the compiler, the false part will be removed
        //      when code is compiled in the editor, first method, otherwise second method
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
