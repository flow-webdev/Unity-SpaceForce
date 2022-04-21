using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;

    public int lifes = 3;
    public int points = 0;

    private float elapsedTime = 0;
    private float timeLimit = 8f;

    void Start() {
        playerController = FindObjectOfType<PlayerController>(true);
    }

    
    void Update() {
        CheckDeath();
    }

    void CheckDeath() {

        if(!playerController.isAlive) {
            elapsedTime += Time.deltaTime;
            StartCoroutine(Restarting());
        }
    }


    private IEnumerator Restarting() {

        if (elapsedTime <= timeLimit) {
            yield return new WaitForSeconds(4f);
            playerController.gameObject.SetActive(true);
            playerController.ActivateShield(4f);

            //while (elapsedTime <= timeLimit) {
            //    yield return new WaitForSeconds(0.5f);
            //    playerController.gameObject.SetActive(false);
            //    yield return new WaitForSeconds(0.5f);
            //    playerController.gameObject.SetActive(true);
            //}
        }
        playerController.isAlive = true;
        elapsedTime = 0;
    }

}


//while (true) {
//    yield return new WaitForSeconds(0.5f);
//    playerController.gameObject.GetComponentInChildren<Renderer>().enabled = false;
//    playerController.gameObject.GetComponent<Collider>().enabled = false;
//    yield return new WaitForSeconds(0.5f);
//    playerController.gameObject.GetComponentInChildren<Renderer>().enabled = true;
//    playerController.gameObject.GetComponent<Collider>().enabled = true;
//}
