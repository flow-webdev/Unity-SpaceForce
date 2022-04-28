using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public abstract class EnemyScript : MonoBehaviour {
    public PlayerController playerController;
    public GameObject player;
    public GameObject baseExplosion;
    public LevelManager levelManager;

    public GameObject placeholderLeft;
    public GameObject placeholderRight;
    public GameObject placeholderCenter;

    public AudioSource audioSource;
    public AudioClip shootingSound;

    private float verticalOffscreen = 15f;
    private float horizontalOffscreen = 55f;
    public float speed = 5;
    public float timeRemaining = 6f;

    protected virtual void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>(true); // With true find GameObject active or inactive
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        player = GameObject.Find("Player");
        this.GetComponent<Rigidbody>().sleepThreshold = 0; // Without this, when not moving, trigger is not detected

        audioSource = GetComponent<AudioSource>();
        //explodeSound = Resources.Load<AudioClip>("Sounds/projectsu012__boom5");

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

        if (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Projectile Laser")) {
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

        if (collision.gameObject.CompareTag("Player") && !playerController.isShieldActive) {
            Explode();
            playerController.EliminatePlayer();
        } else if (collision.gameObject.CompareTag("Player") && playerController.isShieldActive) {
            Explode();
        }
    }

    protected void Explode() {

        levelManager.PlayExplosionAudio();
        Destroy(gameObject);                
        Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);
        
        if (this.gameObject.name == "Shooter(Clone)" || this.gameObject.name == "Kamikaze(Clone)") {
            GameManager.Instance.UpdateScore(100);
        } else {
            GameManager.Instance.UpdateScore(100);
        }
    }

    protected abstract void Shoot();

    protected abstract void Movement();
}
