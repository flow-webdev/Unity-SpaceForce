using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ProjectilePool projectilePool;
    private ProjectileLaserPool projectileLaserPool;
    private LevelManager levelManager;
    public GameObject baseExplosion;
    public GameObject bombExplosion;
    public GameObject shield;

    private AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip rocketSound;
    public AudioClip laserSound; 

    public bool isAlive = true;
    public bool isBombing = false;
    public bool isShieldActive = false;
    private bool isLaser = false;

    // Powerup affected abilities
    public float speed;      

    // Boundaries
    private float rightLeftBound = 30f;
    private float bottomBound = 8.5f;
    private float topBound = 12f;    

    // Called even if the script is not enable. It allows you to initialize setting for
    // an object before enabling the script component
    void Awake() {}
    // Called after awake immeditely before the first Update()
    void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        projectileLaserPool = FindObjectOfType<ProjectileLaserPool>();
        levelManager = FindObjectOfType<LevelManager>();

        audioSource = GetComponent<AudioSource>();
        speed = GameManager.Instance.speed;
    }

    // Call before rendering a frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.K) && !MenuUIHandler.Instance.isPause) {
            Shoot();            
        }

        if (Input.GetKeyDown(KeyCode.L) && !MenuUIHandler.Instance.isPause) { // Release bomb
            StartBombing();            
        }

        if (Input.GetKeyDown(KeyCode.Space) && !MenuUIHandler.Instance.isPause) { 
            EliminatePlayer();
        }
    }

    // Call before perform any physics calculation (physics code goes here)
    void FixedUpdate() {
        MovePlayer();
        Boundaries();
    }

    public void EliminatePlayer() {   
        gameObject.SetActive(false);
        levelManager.PlayExplosionAudio();
        isAlive = false;
        GameManager.Instance.powerupCount = 0;
        isLaser = false;
        speed = 10f;
        GameManager.Instance.bombs = 3;
        GameManager.Instance.lives -= 1;
        GameManager.Instance.UpdateLives(1);
        Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);        
    }

    void MovePlayer() {

        // Moves Player based on arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // Deactivate forward move if out of bound
        if (transform.position.z >= -bottomBound && transform.position.z <= topBound) {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }

    void Boundaries() {

        //Constrain player position on Z axis
        if (transform.position.z < -bottomBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bottomBound + 0.1f); //Add 0.1, otherwise with the shield it remain stuck
        } else if (transform.position.z > topBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound - 0.1f);
        }
        // Constrain player position on X axis
        if (transform.position.x < -rightLeftBound) {
            transform.position = new Vector3(-rightLeftBound, transform.position.y, transform.position.z);
        } else if (transform.position.x > rightLeftBound) {
            transform.position = new Vector3(rightLeftBound, transform.position.y, transform.position.z);
        }
    }

    // On collision when you try to do something with physics
    // Passes info about the collision to the method as an argument
    void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Asteroid")) {
            Destroy(collision.gameObject);            
            EliminatePlayer();             
        }
    }

    // On trigger when we want objects to pass through each other but register the collision
    // Will run or trigger the code in the method when a RigidBody enters the collider
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Powerup Multi")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            GameManager.Instance.powerupCount += 1;

        } else if (other.gameObject.CompareTag("Powerup Speed")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            if (speed < 20) {
                speed += 3.4f;
            } else {
                GameManager.Instance.points += 100;
            }

        } else if (other.gameObject.CompareTag("Powerup Bomb")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            GameManager.Instance.bombs += 1;

        } else if (other.gameObject.CompareTag("Powerup Life")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            GameManager.Instance.lives += 1;


        } else if (other.gameObject.CompareTag("Powerup Shield")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            ActivateShield();
            StartCoroutine(DeactivateShield());

        } else if (other.gameObject.CompareTag("Powerup Laser")) {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            isLaser = true;
            GameManager.Instance.powerupCount = 0;
        }
    }    

    void Shoot() {

        if (!isLaser) {
            audioSource.PlayOneShot(rocketSound);            
            switch (GameManager.Instance.powerupCount) {

                case 0:
                    GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 2f));
                    break;
                case 1:
                    GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 2f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(1, -1, 0.75f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(-1, -1, 0.75f));
                    break;
                default:
                    GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 3f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(1, -1, 0.75f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(-1, -1, 0.75f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(2, -1, 0.5f));
                    GetPoolProjectile(gameObject.transform.position + new Vector3(-2, -1, 0.5f));
                    break;
            }
        
        } else {
            audioSource.PlayOneShot(laserSound);
            switch (GameManager.Instance.powerupCount) {

                case 0:
                    GetPoolProjectileLaser(gameObject.transform.position + new Vector3(0, -1, 2f));
                    break;
                default:
                    GetPoolProjectileLaser(gameObject.transform.position + new Vector3(0, -1, 2f));
                    GetPoolProjectileLaser(gameObject.transform.position + new Vector3(2, -1, 0.75f));
                    GetPoolProjectileLaser(gameObject.transform.position + new Vector3(-2, -1, 0.75f));
                    break;
            }
        }        
    }


    // Method that return a projectile from the pool
    private GameObject GetPoolProjectile(Vector3 vectorPos) {
        GameObject newProjectile = projectilePool.GetProjectile();
        newProjectile.transform.position = vectorPos;
        return newProjectile;
    }

    private GameObject GetPoolProjectileLaser(Vector3 vectorPos) {
        GameObject newProjectile = projectileLaserPool.GetProjectile();
        newProjectile.transform.position = vectorPos;
        return newProjectile;
    }

    private void StartBombing() {
        if (GameManager.Instance.bombs > 0) {
            Instantiate(bombExplosion, transform.position, bombExplosion.transform.rotation);
            StartCoroutine(EnemyExplosion());
            StartCoroutine(StopBombing());
            GameManager.Instance.bombs -= 1;
        }
    }

    // Checked by enemy, if true they explode
    private IEnumerator EnemyExplosion() {
        yield return new WaitForSeconds(0.5f);
        isBombing = true;    
    }

    // After 1 sec from the previous, stop the enemies from exploding
    private IEnumerator StopBombing() {
        yield return new WaitForSeconds(0.6f);
        isBombing = false;
    }

    public void ActivateShield() {
        isShieldActive = true;
        shield.SetActive(true);        
    }

    private IEnumerator DeactivateShield() {        
        yield return new WaitForSeconds(10f);
        isShieldActive = false;
        shield.SetActive(false);
    }

    public void DeactivateShieldInstant() {      
        isShieldActive = false;
        shield.SetActive(false);
    }
}