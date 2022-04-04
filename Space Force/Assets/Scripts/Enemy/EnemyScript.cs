using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public PlayerController playerController;    
    public float speed = 5;
    private float verticalOffscreen = 15f;
    private float horizontalOffscreen = 55f;

    void Start() {

        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Projectile")) {
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            playerController.EliminatePlayer();
        }
    }

    public void GoesOffscreen() {

        if (gameObject.transform.position.z < -verticalOffscreen) {
            Destroy(gameObject);
        } else if (gameObject.transform.position.x < -horizontalOffscreen || gameObject.transform.position.x > horizontalOffscreen) {
            Destroy(gameObject);
        }
    }

    public abstract void Shoot();

    public abstract void Movement();
}
