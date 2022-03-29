using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public float speed;
    private Vector3 startPos;
    private float backgroundWidth = 50f;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        
        Move();
        RepeatBaackground();
    }

    void Move() {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void RepeatBaackground() {
        if (transform.position.z < (startPos.z - backgroundWidth)) {
            transform.position = startPos;
        }
    }
}
