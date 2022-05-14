using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40f;
    private float offscreen = 30;
    private ProjectilePool projectilePool;
    private ProjectileLaserPool projectileLaserPool;

    void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        projectileLaserPool = FindObjectOfType<ProjectileLaserPool>();
    }

    public virtual void Update() {

        Movement();

        // Destroy/return to pool offscreen projectile
        if (transform.position.z > offscreen) {
            ReturnToPool();
        }
    }

    public virtual void Movement() {
       
        transform.Translate(Vector3.forward * speed * Time.deltaTime);       
    }

    public virtual void OnTriggerEnter(Collider other) {        
        // TO RETURN TO POOL
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Enemy")) {
            if (this.gameObject.tag == "Projectile") {
                ReturnToPool();
            } else if (this.gameObject.tag == "Projectile Laser") {
                ReturnToPoolLaser();
            }
        }
    }

    void ReturnToPool() {
        if (projectilePool != null) {
            projectilePool.ReturnProjectile(this.gameObject);
        }
    }

    void ReturnToPoolLaser() {
        if (projectileLaserPool != null) {
            projectileLaserPool.ReturnProjectile(this.gameObject);
        }
    }
}




// TO DESTROY PROJECTILE
//if (other.gameObject.CompareTag("Asteroid")) {
//    Destroy(gameObject);
//    Destroy(other.gameObject);
//    //other.gameObject.GetComponent<Rigidbody>().useGravity = true;
//    //Enemy enemy = other.gameObject.GetComponent<Enemy>();
//    //enemy.speed = 0;
//}
