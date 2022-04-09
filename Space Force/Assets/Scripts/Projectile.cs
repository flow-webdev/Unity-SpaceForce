using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private Rigidbody objectRb;
    public float speed = 40f;
    private float offscreen = 30;
    private ProjectilePool projectilePool;

    void Start() {
        //objectRb = GetComponent<Rigidbody>();
        projectilePool = FindObjectOfType<ProjectilePool>();
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
        //objectRb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);
        
    }

    public virtual void OnTriggerEnter(Collider other) {        
        // TO RETURN TO POOL
        if (other.gameObject.CompareTag("Asteroid")) {
            Destroy(other.gameObject);
            ReturnToPool();
        }
    }

    void ReturnToPool() {
        if (projectilePool != null) {
            projectilePool.ReturnProjectile(this.gameObject);
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
