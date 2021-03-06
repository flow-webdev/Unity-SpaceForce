using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    public GameObject player;
    PlayerController playerController;
    Vector3 lastPlayerPos;

    void Start() {
        player = GameObject.Find("Player");
        playerController = FindObjectOfType<PlayerController>();

        lastPlayerPos = player.transform.position; //+ new Vector3(0,0,-20);
    }

    public override void Update() {
        Movement();
    }

    public override void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            playerController.EliminatePlayer();
            Destroy(gameObject);
        }
    }

    public override void Movement() {

        transform.Translate(Vector3.forward * speed * Time.deltaTime); // WORKING!!!!
        //transform.LookAt(lastPlayerPos, Vector3.forward); //QUESTO ERA IL PROBLEMA!
    }
}

