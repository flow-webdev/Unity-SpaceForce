using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ProjectilePool projectilePool;
    public GameObject baseExplosion;
    public GameObject bombExplosion;

    public float speed = 8f;
    private int powerupCount = 0;
    public bool isAlive = true;
    public bool isBombing;

    // Boundaries
    private float rightLeftBound = 20f;
    private float bottomBound = 8.5f;
    private float topBound = 12f;    

    // Called even if the script is not enable. It allows you to initialize setting for
    // an object before enabling the script component
    void Awake() {}
    // Called after awake immeditely before the first Update()
    void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
    }

    // Call before rendering a frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.M)) { // Release bomb
            Instantiate(bombExplosion, transform.position, bombExplosion.transform.rotation);
            StartCoroutine(enemyExplosion());
            StartCoroutine(stopBombing());
        }
    }

    // Call before perform any physics calculation (physics code goes here)
    void FixedUpdate() {
        MovePlayer();
        Boundaries();
    }

    public void EliminatePlayer() {
        gameObject.SetActive(false);
        isAlive = false;
        Instantiate(baseExplosion, transform.position, baseExplosion.transform.rotation);
    }

    // On collision when you try to do something with physics
    // Passes info about the collision to the method as an argument
    void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Asteroid")) {
            Destroy(collision.gameObject);
            powerupCount = 0;
            EliminatePlayer();            
        }
    }

    // On trigger when we want objects to pass through each other but register the collision
    // Will run or trigger the code in the method when a RigidBody enters the collider
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Powerup")) {
            Destroy(other.gameObject);
            powerupCount += 1;
        }
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
            transform.position = new Vector3(transform.position.x, transform.position.y, -bottomBound);
        } else if (transform.position.z > topBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }
        // Constrain player position on X axis
        if (transform.position.x < -rightLeftBound) {
            transform.position = new Vector3(-rightLeftBound, transform.position.y, transform.position.z);
        } else if (transform.position.x > rightLeftBound) {
            transform.position = new Vector3(rightLeftBound, transform.position.y, transform.position.z);
        }
    }

    void Shoot() {

        switch (powerupCount) {

            case 0:
                GetPoolProjectile(gameObject.transform.position + new Vector3(0, 0, 2f));
                break;
            case 1:
                GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 2f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(1, -1, 0.75f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(-1, -1, 0.75f));
                break;
            default:
                GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 2f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(1, -1, 0.75f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(-1, -1, 0.75f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(2, -1, 0.5f));
                GetPoolProjectile(gameObject.transform.position + new Vector3(-2, -1, 0.5f));
                break;
        }
    }


    // Method that return a projectile from the pool
    private GameObject GetPoolProjectile(Vector3 vectorPos) {
        GameObject newProjectile = projectilePool.GetProjectile();
        newProjectile.transform.position = vectorPos;
        return newProjectile;
    }

    // Checked by enemy, if true they explode
    private IEnumerator enemyExplosion() {        
        yield return new WaitForSeconds(0.5f);
        isBombing = true;
    }

    // After 1 sec from the previous, stop the enemies from exploding
    private IEnumerator stopBombing() {
        yield return new WaitForSeconds(0.6f);
        isBombing = false;
    }

}