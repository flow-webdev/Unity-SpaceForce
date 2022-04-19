using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRun : EnemyScript {    

    public GameObject enemyProjectile;

    private float startPos;    
    private float initialXPos = 40f;
    private float elapsedTime; // trascorso
    private float timeLimit = 0.8f;

    protected override void Start() {
        base.Start();        
        startPos = gameObject.transform.position.x;
    }

    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Shoot() {

        if (playerController.isAlive) {
            Instantiate(enemyProjectile, gameObject.transform.position, transform.rotation);
        }
    }

    protected override void Movement() {
        // Player direction - enemy direction will give the exact direction (Vector) for the enemy to
        // follow in order to chase the player. Normalize will not let the enemy increase his force
        // with the speed (if use addForce to rigidBody);
        
        float step = speed * Time.deltaTime;

        if (playerController.isAlive) {

            if (timeRemaining > 3f) { // Go toward the player for 2.5 seconds, then shoot and go offscreen
                timeRemaining -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.LookAt(player.transform);

            } else if (timeRemaining < 3f && timeRemaining > 1) {
                timeRemaining -= Time.deltaTime; // enemy movement time
                ShootingTime();

            } else if (timeRemaining < 1 && startPos < -initialXPos) {
                transform.position = Vector3.MoveTowards(transform.position, placeholderLeft.transform.position, step);
                transform.LookAt(player.transform);

            } else if (timeRemaining < 1 && startPos > initialXPos) {
                transform.position = Vector3.MoveTowards(transform.position, placeholderRight.transform.position, step);
                transform.LookAt(player.transform);
            }
        } else {

            if (startPos < -initialXPos) {
                transform.position = Vector3.MoveTowards(transform.position, placeholderLeft.transform.position, step);
                transform.LookAt(placeholderLeft.transform);

            } else if (startPos > initialXPos) {
                transform.position = Vector3.MoveTowards(transform.position, placeholderRight.transform.position, step);
                transform.LookAt(placeholderRight.transform);
            }
        }

          
    }

    private void ShootingTime() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit) {
            elapsedTime = 0;
            transform.LookAt(player.transform);
            Shoot();
        }
    }

}
