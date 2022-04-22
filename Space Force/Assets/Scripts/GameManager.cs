using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;
    public Material whiteM, blueM;
    private Color white, blue;

    public int lives = 3;
    public int points = 0;

    private float elapsedTime = 0;
    private int timeLimit = 8;
    private Color originalShieldColor;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
        player = GameObject.Find("Player");
        white = whiteM.color;
        blue = blueM.color;
    }

    
    void Update() {
        if (!playerController.isAlive) {
            StartCoroutine(ReviveCoroutine());            
        } else {
            StopAllCoroutines();
        }
    }

    private IEnumerator ReviveCoroutine() {

        yield return new WaitForSeconds(4f);
        if (lives > 0) {
            yield return StartCoroutine(BlinkCoroutine());
        }
        Debug.Log("EXIT FIRST ROUTINE");
        //playerController.DeactivateShieldInstant();
        //playerController.isAlive = true;
        //elapsedTime = 0;
    }

    private IEnumerator BlinkCoroutine() {
        
        playerController.gameObject.SetActive(true);
        playerController.ActivateShield();
        MeshRenderer meshRenderer = GameObject.Find("Shield").GetComponent<MeshRenderer>();
        elapsedTime += Time.deltaTime;

        int seconds = timeLimit - (int)elapsedTime;
        Debug.Log("seconds=" + seconds);

        while (elapsedTime <= timeLimit) {
            yield return new WaitForSeconds(2f);
            if (meshRenderer.material.color == white) {
                meshRenderer.material.color = blue;
            } else if (meshRenderer.material.color == blue) {
                meshRenderer.material.color = white;
            }

            //meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            
        }

        playerController.DeactivateShieldInstant();
        playerController.isAlive = true;
        elapsedTime = 0;
        Debug.Log("LOOP EXIT");
    }

}


//if (elapsedTime % 2 == 0) {
//    meshRenderer.material = blue;
//    yield return new WaitForSeconds(1f);
//} else {
//    meshRenderer.material = white;
//    yield return new WaitForSeconds(1f);
//}
//meshRenderer.material = white ? blue : white;
//yield return new WaitForSeconds(1f);
//meshRenderer.material = white ? blue : white;
//yield return new WaitForSeconds(1f);

//Debug.Log(elapsedTime);
//meshRenderer.material = white? blue : white;
//yield return new WaitForSeconds(1f);