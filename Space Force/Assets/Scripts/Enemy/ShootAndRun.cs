using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRun : EnemyScript {

    private GameObject player;
    private GameObject placeholderLeft;
    private GameObject placeholderRight;
    private float startPos;
    private float timeRemaining = 2;

    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>(); // Keep parent reference, otherwise NullReferenceException
        player = GameObject.Find("Player");
        placeholderLeft = GameObject.Find("Placeholder Left");
        placeholderRight = GameObject.Find("Placeholder Right");
        startPos = gameObject.transform.position.x;
        
    }

    //void Update() {
    //    time = Time.deltaTime;
    //}

    void FixedUpdate() {
        Movement();
    }

    // Need to override, otherwise is not able to call the method in PlayerController
    public override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);           
    }

    public override void Shoot() {}

    public override void Movement() {
        // Player direction - enemy direction will give the exact direction (Vector) for the enemy to
        // follow in order to chase the player. Normalize will not let the enemy increase his force
        // with the speed (if use addForce to rigidBody);
        
        //Vector3 lookDirection = (player.transform.position - transform.position);

        //if (startPos < -40 && gameObject.transform.position.x > -16) {
        //    lookDirection = (placeholderLeft.transform.position - transform.position);
            
        //} else if (startPos > 40 && gameObject.transform.position.x < 16) {
        //    lookDirection = (placeholderLeft.transform.position - transform.position);
        //}

        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            Vector3 lookDirection = (player.transform.position - transform.position);
            transform.Translate(lookDirection * speed * Time.deltaTime);
        } else if (timeRemaining < 0 && startPos < -40) {
            Vector3 lookDirection = (placeholderLeft.transform.position - transform.position);
            transform.Translate(lookDirection * speed * Time.deltaTime);
        }

        //transform.Translate(lookDirection * speed * Time.deltaTime);
    }
}
