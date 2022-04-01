using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRun : EnemyScript {

    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    void FixedUpdate() {
        Movement();
    }

    public override void Shoot() {}

    public override void Movement() {
        // Player direction - enemy direction will give the exact direction (Vector) for the enemy to
        // follow in order to chase the player. Normalize will not let the enemy increase his force
        // with the speed (if use addForce to rigidBody);
        Vector3 lookDirection = (player.transform.position - transform.position);
        transform.Translate(lookDirection * speed * Time.deltaTime);
    }
}
