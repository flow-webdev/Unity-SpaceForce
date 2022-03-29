using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 5f;
    private float offScreen = -35;
    private Rigidbody objectRb;
    private Vector3[] asteroidRotations;
    int index;

    void Start() {

        //Enemy rb
        objectRb = GetComponent<Rigidbody>();

        // Array of different rotations
        asteroidRotations = AllRotations();

        // Index to be used for randomly choose the rotation
        index = IndexGenerator();
    }

    void Update() {

        Movement(index);

        // Destroy offscreen objects
        if (transform.position.z < offScreen) {
            Destroy(gameObject);
        }
    }

    void Movement(int vectorIndex) {

        // Asteroid movement and rotation
        objectRb.AddForce(Vector3.back.normalized * speed);
        objectRb.AddTorque(asteroidRotations[vectorIndex]);
    }

    // Random index generator
    int IndexGenerator() {
        return Random.Range(0, asteroidRotations.Length);
    }

    Vector3[] AllRotations() {
        Vector3[] astRotations = new Vector3[6];
        astRotations[0] = Vector3.up * 2;
        astRotations[1] = Vector3.down * 2;
        astRotations[2] = Vector3.right * 2;
        astRotations[3] = Vector3.left * 2;
        astRotations[4] = Vector3.forward * 2;
        astRotations[5] = Vector3.back * 2;

        return astRotations;
    }
}
