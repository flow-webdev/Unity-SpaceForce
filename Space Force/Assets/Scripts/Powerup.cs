using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float speed = 3f;
    private float offScreen = -35;

    void Update() {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        transform.Rotate(Vector3.back, Space.Self);

        // Destroy offscreen objects
        if (transform.position.z < offScreen) {
            Destroy(gameObject);
        }
    }
}
