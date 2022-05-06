using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 startPos;
    private float backgroundWidth = 50f;

    public Material levelOneMaterial;
    public Material levelTwoMaterial;
    public Material levelThreeMaterial;

    private void Awake() {
        CheckScene();
    }

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        
        Move();
        RepeatBaackground();
    }

    private void CheckScene() {
        Scene scene = SceneManager.GetActiveScene();
        
        if (scene.name == "Level 1") {
            ChangeChildMaterial(levelOneMaterial);
        
        } else if (scene.name == "Level 2") {
            ChangeChildMaterial(levelTwoMaterial);
        
        } else if (scene.name == "Level 3") {
            ChangeChildMaterial(levelThreeMaterial);
        }
    }

    private void ChangeChildMaterial(Material material) {
        List<Material> currentMat = new List<Material>();
        currentMat.Add(material);
        int numOfChildren = transform.childCount;

        for (int i = 0; i < numOfChildren; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().materials = currentMat.ToArray();
        }
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
