using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    public override void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
        }
    }

    public override void Movement() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
