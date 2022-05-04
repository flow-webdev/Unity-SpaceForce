using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private PlayerController playerController;
    public Material white, red;

    [SerializeField] AudioSource audioSource;
    public AudioClip explodeSound;
    //public AudioClip winnerSound;
    //public AudioClip loserSound;

    public bool isChangeColor = false; // enchance color change during coroutine
    private bool reviveCoroutineRunning = false; // Without this bool, coroutine starts every frame    
    private int blinkCounter = 10;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);

        // Apparently Coroutine perform better than Invoke/InvokeRepeating
        StartCoroutine(TimerCoroutine()); //GameManager.Instance.InvokeRepeating("UpdateTime", 1, 1);
    }

    private IEnumerator TimerCoroutine() {

        while (GameManager.Instance.time >= 1) {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.UpdateTime();
        }
    }

    void Update() {

        if (GameManager.Instance.lives <= 0) { // GameOver if player has no lives CHIEDERE && !GameManager.Instance.isGameOver
            GameOver();
        }
        else if (!playerController.isAlive && !reviveCoroutineRunning && GameManager.Instance.lives > 0) { // Reactivate player if has lives left
            StartCoroutine(ReviveCoroutine());
        }

        if (GameManager.Instance.time <= 0 && playerController.isAlive) { // Victory if time is 0
            Victory();            
        }

        if (Input.GetKeyDown(KeyCode.P) && !MenuUIHandler.Instance.isPause) { // Pause game
            MenuUIHandler.Instance.OnPause();
            PauseGame();
        }
    }

    private IEnumerator ReviveCoroutine() { // First Coroutine just wait 3 sec
        reviveCoroutineRunning = true;
        yield return new WaitForSeconds(3f);
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine() { // Second Coroutine activate player, shield and blink color every 0.4f

        playerController.gameObject.SetActive(true);
        playerController.ActivateShield();
        MeshRenderer meshRenderer = GameObject.Find("Shield").GetComponent<MeshRenderer>();

        for (int i = 0; i < blinkCounter; i++) {
            isChangeColor = !isChangeColor;
            yield return new WaitForSeconds(0.4f);
            if (isChangeColor) {
                meshRenderer.material = red;
            } else {
                meshRenderer.material = white;
            }
        }

        // Bring back everything to standard
        meshRenderer.material = white;
        playerController.isAlive = true;
        playerController.DeactivateShieldInstant();
        isChangeColor = false;
        reviveCoroutineRunning = false;
    }

    public void PlayExplosionAudio() {
        // Need to play the sound here, otherwise the obj (and the sound) get destroyed before it's heard
        audioSource.PlayOneShot(explodeSound);
    }

    public void PauseGame() {
        // Toggle on/off pause by the variable in MenuUI
        if (MenuUIHandler.Instance.isPause) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void GameOver() {
        GameManager.Instance.isGameOver = true;
        MenuUIHandler.Instance.OnGameOver();
        //audioSource.Stop();
        //audioSource.loop = false;
        //audioSource.clip = loserSound;
        //audioSource.Play();
    }

    public void Victory() {
        GameManager.Instance.isVictory = true;
        GameManager.Instance.UpdatePointsVictory();
        MenuUIHandler.Instance.OnVictory();
    }


}