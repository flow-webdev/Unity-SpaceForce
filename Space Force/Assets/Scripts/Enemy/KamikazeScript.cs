using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeScript : EnemyScript {

    public GameObject enemyProjectile;
    private float timeRemaining = 6f;

    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        player = GameObject.Find("Player");
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

    protected override void Shoot() {

        if (playerController.isAlive) {
            Instantiate(enemyProjectile, gameObject.transform.position, transform.rotation);
        }
    }

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (timeRemaining > 3) {
            timeRemaining -= Time.deltaTime;
            transform.Translate(Vector3.forward * step);

        } else {
            speed = 15;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
        }
    }

}
