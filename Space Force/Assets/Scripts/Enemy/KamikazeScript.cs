using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeScript : EnemyScript {

    public GameObject enemyProjectile;
    private GameObject placeholderCenter;
    private float timeRemaining = 6f;
    private bool alive = false;

    void Start() {

        player = GameObject.Find("Player");
        playerController = GameObject.FindObjectOfType<PlayerController>(true); // With true find GameObject active or inactive
        placeholderCenter = GameObject.Find("Placeholder Center");
        this.GetComponent<Rigidbody>().sleepThreshold = 0; // Without this, when not moving, trigger is not detected        
    }

    void Update() {
        GoesOffscreen();        
    }

    void FixedUpdate() {
        Movement();
    }

    // Need to override, otherwise is not able to call the method in PlayerController EliminatePlayer()
    protected override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
    }

    protected override void Shoot() {}

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (playerController.isAlive) {
            if (timeRemaining > 3) {
                timeRemaining -= Time.deltaTime;
                transform.Translate(Vector3.forward * step);

            } else {
                speed = 15;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.LookAt(player.transform);
            }
        } else if (!playerController.isAlive) {
            speed = 15;
            transform.position = Vector3.MoveTowards(transform.position, placeholderCenter.transform.position, step);
            transform.LookAt(placeholderCenter.transform);
        }

        
    }

}
