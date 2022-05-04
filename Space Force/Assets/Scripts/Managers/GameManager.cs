using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float speed = 15f;
    public int lives = 3;
    public int points = 0;
    public int powerupCount = 0;
    public int bombs = 3;
    public bool isGameOver = false;
    public bool isVictory = false; // Alert EnemyScript and SpawnManager to stop
    public int time = 150;

    [SerializeField] private Text livesText;
    [SerializeField] private Text bombsText;
    [SerializeField] private Text pointsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text pointsTextVictory;

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

    // Text component is in another scene, so I need to use SceneManager.sceneLoaded to get the reference
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Debug.Log("Scene name: " + scene.name);
        //Debug.Log(mode);

        if (scene.name == "Level 1") {
            livesText = GameObject.Find("Lives").GetComponent<Text>();
            bombsText = GameObject.Find("Bombs").GetComponent<Text>();
            pointsText = GameObject.Find("Points").GetComponent<Text>();
            timeText = GameObject.Find("Time").GetComponent<Text>();
        }        
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetNewGame() { // Called by MenuUIHandler if GameOver button is clicked
        speed = 15f;
        lives = 3;
        points = 0;
        powerupCount = 0;
        bombs = 3;
        isGameOver = false;
        isVictory = false;
        time = 150;
    }

    public void UpdateTime() { // Timer called by LevelManager every second
        time -= 1;
        timeText.text = "TIME : " + time;

        if (time < 150) {
            MenuUIHandler.Instance.home.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int scoreToAdd) {
        points += scoreToAdd;
        pointsText.text = "POINTS : " + points;
    }

    public void UpdatePointsVictory() {
        pointsTextVictory.text = "POINTS : " + points;
    }

    public void UpdateLives(int livesToAdd, bool isTrue) {
        if (isTrue) {
            lives += livesToAdd;
        } else {
            lives -= livesToAdd;
        }        
        livesText.text = "LIVES : " + lives;
    }

    public void UpdateBombs(int bombsToAdd, bool isTrue) {
        if (isTrue) {
            bombs += bombsToAdd;
        } else {
            bombs -= bombsToAdd;
        }
        bombsText.text = "BOMBS : " + bombs;
    }

}
