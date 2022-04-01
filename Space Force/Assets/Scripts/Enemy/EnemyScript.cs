using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public float speed = 5;

    public abstract void Shoot();

    public abstract void Movement();

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Projectile")) {
            Destroy(gameObject);
        }
    }
}
