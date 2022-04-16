using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    public GameObject baseExplosion;

    public float speed = 5;
    private float verticalOffscreen = 15f;
    private float horizontalOffscreen = 55f;

    // Range -42 o 42, 10, 34

    void Start() {

        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Update() {
    }

    protected void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Projectile")) {
            Destroy(gameObject);
            Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);
        }
    }

    protected void GoesOffscreen() {

        if (gameObject.transform.position.z < -verticalOffscreen) {
            Destroy(gameObject);
        } else if (gameObject.transform.position.x < -horizontalOffscreen || gameObject.transform.position.x > horizontalOffscreen) {
            Destroy(gameObject);
        }
    }


    protected virtual void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);
            playerController.EliminatePlayer();
        }
    }

    protected abstract void Shoot();

    protected abstract void Movement();
}
