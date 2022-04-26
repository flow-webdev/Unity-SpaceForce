using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //public GameObject pause;
    //public bool isPause = false;

    public float speed = 10f;
    public int lives = 3;
    public int points = 0;
    public int powerupCount = 0;
    public int bombs = 3;

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
