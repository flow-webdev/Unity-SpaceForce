using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float speed = 10f;
    public int lives = 1;
    public int points = 0;
    public int powerupCount = 0;
    public int bombs = 3;
    public bool isGameOver = false;

    [SerializeField] public Text livesText;
    [SerializeField] private Text bombsText;
    [SerializeField] private Text pointsText;

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

    private void Start() {
        livesText = GameObject.Find("Lives").GetComponent<Text>();
    }

    public void ResetNewGame() {
        speed = 10f;
        lives = 1;
        points = 0;
        powerupCount = 0;
        bombs = 3;
        isGameOver = false;    
    }

    public void UpdateScore(int scoreToAdd) {
        points += scoreToAdd;
        pointsText.text = "Points : " + points;
    }

    public void UpdateLives(int livesToSubtract) {
        lives += livesToSubtract;
        livesText.text = "Lives : " + lives;
    }

}
