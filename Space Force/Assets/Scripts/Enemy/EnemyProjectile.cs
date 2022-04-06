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

        if (playerController.isAlive) {
            lastPlayerPos = player.transform.position + new Vector3(0,0,-20);
        } else {
            lastPlayerPos = new Vector3(0, 10, -4);
        }
    }

    public override void Update() {
        Movement();
    }

    public override void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            playerController.EliminatePlayer();

            if (playerController.isAlive) {
                Destroy(gameObject);
            } else {
                //gameObject.GetComponent<BoxCollider>().isTrigger = false;
                Destroy(gameObject);
            }
        }
    }

    public override void Movement() {

        //Vector3 lastPlayerPos = player.transform.position;
        Vector3 moveDirection = (lastPlayerPos - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
        transform.LookAt(lastPlayerPos);
        //Debug.Log("lastPlayerPos= " + lastPlayerPos);

        //if (playerController.isAlive) {



        //} else {
        //    Vector3 moveDirection = (lastPlayerPos - transform.position).normalized;
        //    transform.position += moveDirection * speed * Time.deltaTime;
        //    transform.LookAt(lastPlayerPos);
        //    //Debug.Log("lastPlayerPos= " + lastPlayerPos);
        //}
    }
}
