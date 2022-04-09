using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRun : EnemyScript {

    private GameObject player;
    private GameObject placeholderLeft;
    private GameObject placeholderRight;

    public GameObject enemyProjectile;

    private float startPos;
    private float timeRemaining = 6f;
    private float initialXPos = 40f;

    private float elapsedTime; // trascorso
    private float timeLimit = 0.5f;

    void Start() {
        // Did not declared playerController because is declared in the parent class
        playerController = GameObject.FindObjectOfType<PlayerController>(); // Keep parent reference, otherwise NullReferenceException
        player = GameObject.Find("Player");
        placeholderLeft = GameObject.Find("Placeholder Left");
        placeholderRight = GameObject.Find("Placeholder Right");
        startPos = gameObject.transform.position.x;
    }

    void Update() {
        GoesOffscreen();
    }

    void FixedUpdate() {
        Movement();
    }

    // Need to override, otherwise is not able to call the method in PlayerController
    public override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);           
    }

    public override void Shoot() {

        if (playerController.isAlive) {
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(0, 0, 0), transform.rotation);
        }
    }

    private IEnumerator Shooting() {
        yield return new WaitForSeconds(1f);
        Shoot();
    }

    public override void Movement() {
        // Player direction - enemy direction will give the exact direction (Vector) for the enemy to
        // follow in order to chase the player. Normalize will not let the enemy increase his force
        // with the speed (if use addForce to rigidBody);
        
        float step = speed * Time.deltaTime;
        if (timeRemaining > 3.5f ) { // Go toward the player for 3 seconds, then shoot and go offscreen
            timeRemaining -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
        } else if (timeRemaining < 3.5f && timeRemaining > 1) {
            timeRemaining -= Time.deltaTime; // enemy movement time
            elapsedTime += Time.deltaTime;   // shooting time
            if (elapsedTime >= timeLimit) {
                elapsedTime = 0;
                transform.LookAt(player.transform);
                Shoot();
            }
        } else if (timeRemaining < 1 && startPos < -initialXPos) {
            transform.position = Vector3.MoveTowards(transform.position, placeholderLeft.transform.position, step);
            transform.LookAt(player.transform);
        } else if (timeRemaining < 1 && startPos > initialXPos) {
            transform.position = Vector3.MoveTowards(transform.position, placeholderRight.transform.position, step);
            transform.LookAt(player.transform);
        }
    }
}
