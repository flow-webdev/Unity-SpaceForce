using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public PlayerController playerController;
    public float speed = 5;
    private float verticalOffscreen = 15f;

    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Update() {
        GoesOffscreen();
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

    void GoesOffscreen() {
        if (gameObject.transform.position.z < -verticalOffscreen) {
            Destroy(gameObject);
        }
    }

    public abstract void Shoot();

    public abstract void Movement();
}
