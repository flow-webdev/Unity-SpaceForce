using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private PlayerController playerController;
    private SpawnManager spawnManager;
    public Material white, red;

    [SerializeField] AudioSource audioSource;
    public AudioClip explodeSound;

    public bool isChangeColor = false; // enchance color change of shield during revive coroutine
    private bool reviveCoroutineRunning = false; // Without this bool, coroutine starts every frame    
    private int blinkCounter = 10;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
        spawnManager = FindObjectOfType<SpawnManager>(true);
    }

    void Update() {

        if (GameManager.Instance.lives <= 0) { // by adding "&& !GameManager.Instance.isGameOver" the gameOver canvas doesn't work.It need to run 
            GameOver();                        // every frame/second in order to change canvas alpha, otherwise is not changing.
        }
        else if (!playerController.isAlive && !reviveCoroutineRunning && GameManager.Instance.lives > 0) { // Reactivate player if has lives left
            StartCoroutine(ReviveCoroutine());
        }

        if (GameManager.Instance.time <= 0 && playerController.isAlive && spawnManager.sceneName != "Level 3") { // Victory if time is 0 and is not last level or called by BossScript
            Victory();
        }

        if (Input.GetKeyDown(KeyCode.P) && !MenuUIHandler.Instance.isPause) { // Pause game
            MenuUIHandler.Instance.OnPause();
            PauseGame();
        }
    }

    public void GameOver() {
        GameManager.Instance.isGameOver = true;
        MenuUIHandler.Instance.OnGameOver();
    }

    public void Victory() {
        GameManager.Instance.isVictory = true;
        GameManager.Instance.UpdatePointsVictory();
        MenuUIHandler.Instance.OnVictory();
    }

    public void LastVictory() {
        StartCoroutine(LastVictoryCoroutine());
    }

    private IEnumerator LastVictoryCoroutine() {
        yield return new WaitForSeconds(2f);
        Victory();
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


}