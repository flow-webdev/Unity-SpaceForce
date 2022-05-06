using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : EnemyScript 
{

    public GameObject enemyProjectile;
    [SerializeField] private GameObject mine;
    private float elapsedTime; // trascorso
    private float timeLimit = 1.2f;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Shoot() {

        if (playerController.isAlive) {
            audioSource.PlayOneShot(shootingSound);
            Instantiate(mine, gameObject.transform.position + new Vector3(0, 0, -4), transform.rotation);
            Instantiate(mine, gameObject.transform.position + new Vector3(3, 0, -4), transform.rotation);
            Instantiate(mine, gameObject.transform.position + new Vector3(-3, 0, -4), transform.rotation);
            Instantiate(mine, gameObject.transform.position + new Vector3(6, 0, -4), transform.rotation);
            Instantiate(mine, gameObject.transform.position + new Vector3(-6, 0, -4), transform.rotation);
        }
    }

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (timeRemaining > 2) {
            timeRemaining -= Time.deltaTime;
            transform.Translate(Vector3.forward * step);

        } else if (timeRemaining < 2 && timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            ShootingTime();

        } else {
            speed = 15;
            transform.Translate(Vector3.forward * step);
        }
    }

        private void ShootingTime() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit) {
            elapsedTime = 0;
            Shoot();
        }
    }
}
