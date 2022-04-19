using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject player;
    public GameObject baseExplosion;

    public GameObject placeholderLeft;
    public GameObject placeholderRight;
    public GameObject placeholderCenter;

    private float verticalOffscreen = 15f;
    private float horizontalOffscreen = 55f;
    public float speed = 5;
    public float timeRemaining = 6f;

    protected virtual void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>(true); // With true find GameObject active or inactive
        player = GameObject.Find("Player");
        this.GetComponent<Rigidbody>().sleepThreshold = 0; // Without this, when not moving, trigger is not detected

        placeholderLeft = GameObject.Find("Placeholder Left");
        placeholderRight = GameObject.Find("Placeholder Right");
        placeholderCenter = GameObject.Find("Placeholder Center");
    }

    protected virtual void Update() {
        GoesOffscreen();

        if (playerController.isBombing) {
            Explode();
        }
    }

    protected virtual void FixedUpdate() {
        Movement();
    }

    protected void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Projectile")) {
            Explode();
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
            Explode();
            playerController.EliminatePlayer();
        }
    }

    public void Explode() {
        Destroy(gameObject);
        Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);
    }

    protected abstract void Shoot();

    protected abstract void Movement();
}
