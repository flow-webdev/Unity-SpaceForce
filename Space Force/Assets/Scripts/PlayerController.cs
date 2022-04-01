using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    private ProjectilePool projectilePool;
    public float speed = 8f;
    private int powerupCount = 0;

    // Boundaries
    private float rightLeftBound = 13f;
    private float bottomBound = 8.5f;
    private float topBound = 12f;    

    // Called even if the script is not enable. It allows you to initialize setting for
    // an object before enabling the script component
    void Awake() {}
    // Called after awake immeditely before the first Update()
    void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        //moveForward = Find
    }

    // Call before rendering a frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    // Call before perform any physics calculation (physics code goes here)
    void FixedUpdate() {
        MovePlayer();
        Boundaries();
    }

    // On collision when you try to do something with physics
    // Passes info about the collision to the method as an argument
    void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Asteroid")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            powerupCount = 0;            
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
                GetPoolProjectile(gameObject.transform.position + new Vector3(0, -1, 2f));
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
}


// SHOOT MADE WITH INSTANTIATION
//void Shoot() {

//    switch (powerupCount) {

//        case 0:
//            Instantiate(projectile, gameObject.transform.position + new Vector3(0, 0, 2f), projectile.transform.rotation);
//            break;
//        case 1:
//            Instantiate(projectile, gameObject.transform.position + new Vector3(0, 0, 2f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(1, 0, 0.75f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(-1, 0, 0.75f), projectile.transform.rotation);
//            break;
//        default:
//            Instantiate(projectile, gameObject.transform.position + new Vector3(0, 0, 2f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(1, 0, 0.75f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(-1, 0, 0.75f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(2, 0, 0.5f), projectile.transform.rotation);
//            Instantiate(projectile, gameObject.transform.position + new Vector3(-2, 0, 0.5f), projectile.transform.rotation);
//            break;
//    }

//}