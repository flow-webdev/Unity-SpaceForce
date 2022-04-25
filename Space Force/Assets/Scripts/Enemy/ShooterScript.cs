using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : EnemyScript {

    public GameObject enemyProjectile;
    private float elapsedTime; // trascorso
    private float timeLimit = 0.9f;

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
            Instantiate(enemyProjectile, gameObject.transform.position, transform.rotation);
        }
    }

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (playerController.isAlive) {
            if (timeRemaining > 3) {
                timeRemaining -= Time.deltaTime;
                transform.Translate(Vector3.forward * step);

            } else if (timeRemaining < 3 && timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                ShootingTime();

            } else {
                speed = 15;
                transform.Translate(Vector3.forward * step);
            }
        } else if (!playerController.isAlive) {
            speed = 15;
            transform.position = Vector3.MoveTowards(transform.position, placeholderCenter.transform.position, step);
            transform.LookAt(placeholderCenter.transform);
        }
    }

    private void ShootingTime() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit) {
            elapsedTime = 0;
            transform.LookAt(player.transform);
            Shoot();
        }
    }

    

}
