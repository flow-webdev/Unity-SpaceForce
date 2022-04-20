using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    public GameObject player;
    PlayerController playerController;

    void Start() {
        player = GameObject.Find("Player");
        playerController = FindObjectOfType<PlayerController>();
    }

    public override void Update() {
        Movement();
    }

    public override void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player") && !playerController.isShieldActive) {
            playerController.EliminatePlayer();
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Player") && playerController.isShieldActive) {
            Destroy(gameObject);
        }
    }

    public override void Movement() {

        transform.Translate(Vector3.forward * speed * Time.deltaTime); // WORKING!!!!
        //transform.LookAt(lastPlayerPos, Vector3.forward); //QUESTO ERA IL PROBLEMA!
    }
}

