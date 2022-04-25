using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake() {
        if (Instance != null) {
            // If there is no Instance, it will be created, otherwise you will have a new Instances every time the scene change.
            // Singleton pattern, make sure that only a single instance of an object exist.
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // This code enables you to access the MainManager object from any other script. 
    }


}
