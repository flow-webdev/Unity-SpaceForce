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
    public bool isLaser = false;
    public bool isGameOver = false;
    public bool isVictory = false; // Alert EnemyScript and SpawnManager to stop
    public int time;

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

    public void ResetNewGame() { // Called by MenuUIHandler if GameOver button is clicked
        speed = 15f;
        lives = 3;
        points = 0;
        powerupCount = 0;
        bombs = 3;
        isGameOver = false;
        isVictory = false;
        time = 140;
    }

    public void StartLevel() { // Called by UIHandler when start new game or new level        
        isVictory = false;
        time = 140;//////////////////////////////////////////////////////////////////////////////////////////////////////

        // Apparently Coroutine perform better than Invoke/InvokeRepeating
        StartCoroutine(TimerCoroutine()); //GameManager.Instance.InvokeRepeating("UpdateTime", 1, 1);
    }

    private IEnumerator TimerCoroutine() {
        while (time >= 1 && !isGameOver) {
            yield return new WaitForSeconds(1f);
            UpdateTime();
        }
    }

    public void UpdateTime() { // Timer called by LevelManager every second
        time -= 1;
        timeText.text = "TIME : " + time;        

        if (time < 140) {
            MenuUIHandler.Instance.home.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int scoreToAdd) {
        points += scoreToAdd;
        pointsText.text = "POINTS : " + points;
    }

    public void UpdatePointsVictory() { // Called by levelManager
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
