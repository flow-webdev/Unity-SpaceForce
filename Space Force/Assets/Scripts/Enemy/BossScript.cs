using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript {

    public GameObject enemyProjectile;
    private float elapsedTime; // trascorso
    private float timeLimit = 0.7f;

    [SerializeField] private int projectileCount = 0;
    private bool bombboolean = false;
    private int shootCounter = 0;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        //base.Update();
        GoesOffscreen();

        if (playerController.isBombing && !bombboolean) {
            bombboolean = true;
            projectileCount += 6;
            StartCoroutine(ChangeBoolean());
        }
    }

    private IEnumerator ChangeBoolean() {
        yield return new WaitForSeconds(0.2f);
        bombboolean = false;
        if (projectileCount >= 40) {
            Explode();
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Shoot() {

        if (projectileCount < 20) {
            audioSource.PlayOneShot(shootingSound);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(0, 0, -7), transform.rotation);
        } else {
            audioSource.PlayOneShot(shootingSound);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(0, 0, -7), transform.rotation);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(5, 0, -7), transform.rotation);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(-5, 0, -7), transform.rotation);
        }        
    }

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (timeRemaining > 3) {
            timeRemaining -= Time.deltaTime;
            transform.Translate(Vector3.forward * step);
        } else {
            timeRemaining -= Time.deltaTime;
            //StartCoroutine(ShootingCoroutine());
            ShootingTime();
        }
    }

    private IEnumerator ShootingCoroutine() {
        while(shootCounter < 3) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeLimit) {
                elapsedTime = 0;
                Shoot();
                shootCounter++;
            }
            Debug.Log(shootCounter);
            yield return new WaitForSeconds(2f);
            
        }
        yield return new WaitForSeconds(2f);
    }

    private void ShootingTime() {
        if (shootCounter < 3) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeLimit) {
                elapsedTime = 0;
                Shoot();
                shootCounter++;
                Debug.Log(shootCounter);
            }
        }            
    }

}
