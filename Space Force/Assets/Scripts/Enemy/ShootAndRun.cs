using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRun : EnemyScript {

    private GameObject player;
    private GameObject placeholderLeft;
    private GameObject placeholderRight;
    public GameObject enemyProjectile;
    private float startPos;
    private float timeRemaining = 3f;
    private float initialXPos = 40f;

    void Start() {
        // Did not declared playerController because is declared in the parent class
        playerController = GameObject.FindObjectOfType<PlayerController>(); // Keep parent reference, otherwise NullReferenceException
        player = GameObject.Find("Player");
        placeholderLeft = GameObject.Find("Placeholder Left");
        placeholderRight = GameObject.Find("Placeholder Right");
        startPos = gameObject.transform.position.x;

        InvokeRepeating("Shoot", 1f, 0.5f);
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
        float step = speed * Time.deltaTime;
        Quaternion bulletRotation = Quaternion.Inverse(gameObject.transform.rotation);
        Instantiate(enemyProjectile, gameObject.transform.position, gameObject.transform.rotation);
    }

    public override void Movement() {
        // Player direction - enemy direction will give the exact direction (Vector) for the enemy to
        // follow in order to chase the player. Normalize will not let the enemy increase his force
        // with the speed (if use addForce to rigidBody);

        // Go toward the player for 2 seconds, then change direction and go offscreen
        float step = speed * Time.deltaTime;
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
        } else if (timeRemaining < 0 && startPos < -initialXPos) {
            transform.position = Vector3.MoveTowards(transform.position, placeholderLeft.transform.position, step);
            transform.LookAt(player.transform);
        } else if (timeRemaining < 0 && startPos > initialXPos) {
            transform.position = Vector3.MoveTowards(transform.position, placeholderRight.transform.position, step);
            transform.LookAt(player.transform);
        }
    }
}
