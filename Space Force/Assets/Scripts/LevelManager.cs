using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private PlayerController playerController;
    public Material white, red;

    private AudioSource audioSource;
    public AudioClip explodeSound;   

    public bool isChangeColor = false; // enchance color change during coroutine
    private bool reviveCoroutineRunning = false; // Without this bool, coroutine starts every frame    
    private int blinkCounter = 10;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {

        if (GameManager.Instance.lives <= 0) { // GameOver if player has no lives
            GameOver();
        }
        else if (!playerController.isAlive && !reviveCoroutineRunning && GameManager.Instance.lives > 0) { // Reactivate player if has lives
            StartCoroutine(ReviveCoroutine());
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
        //else {
        //    GameManager.Instance.isGameOver = true;
        //    MenuUIHandler.Instance.OnGameOver();
        //}
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
    }


}