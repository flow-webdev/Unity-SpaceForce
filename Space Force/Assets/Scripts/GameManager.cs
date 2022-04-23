using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    public Material white, red;

    public int lives = 3;
    public int points = 0;

    public bool superBool = false;
    private bool reviveRunning = false;
    private int blinkCounter = 10;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
        player = GameObject.Find("Player");
    }


    void Update() {
        //if (Input.GetKeyDown(KeyCode.K)) {
        //    playerController.EliminatePlayer();
        //}
        if (!playerController.isAlive && !reviveRunning) {
            StartCoroutine(ReviveCoroutine());
        }
    }

    private IEnumerator ReviveCoroutine() {
        reviveRunning = true;
        yield return new WaitForSeconds(3f);
        if (lives > 0) {
            StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine() {

        playerController.gameObject.SetActive(true);
        playerController.ActivateShield();
        MeshRenderer meshRenderer = GameObject.Find("Shield").GetComponent<MeshRenderer>();

        for (int i = 0; i < blinkCounter; i++) {
            superBool = !superBool;
            yield return new WaitForSeconds(0.4f);
            if (superBool) {
                meshRenderer.material = red;
            } else {
                meshRenderer.material = white;
            }
        }

        meshRenderer.material = white;
        playerController.isAlive = true;
        playerController.DeactivateShieldInstant();
        superBool = false;
        reviveRunning = false;
    }

}