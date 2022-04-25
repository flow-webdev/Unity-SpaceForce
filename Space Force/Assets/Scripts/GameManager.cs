using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    public Material white, red;

    private AudioSource audioSource;
    public AudioClip explodeSound;
    //audioSource.PlayOneShot(explodeSound);
    //audioSource = GetComponent<AudioSource>();

    public int lives = 3;
    public int points = 0;

    public bool isChangeColor = false; // enchance color change during coroutine
    private bool reviveCoroutineRunning = false; // Without this bool, coroutine starts every frame
    private bool pauseBool = false;
    private int blinkCounter = 10;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {

        if (!playerController.isAlive && !reviveCoroutineRunning) {
            StartCoroutine(ReviveCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            PauseGame();
        }
    }

    private IEnumerator ReviveCoroutine() { // First Coroutine just wait 3 sec
        reviveCoroutineRunning = true;
        yield return new WaitForSeconds(3f);
        if (lives > 0) {
            StartCoroutine(BlinkCoroutine());
        }
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
        // Need to playthe sound here, otherwise the obj (and the sound) get destroyed before it's heard
        audioSource.PlayOneShot(explodeSound);
    }

    public void PauseGame() {

        pauseBool = !pauseBool;

        if (pauseBool) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }        
    }

}