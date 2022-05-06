using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantScript : EnemyScript {

    public GameObject enemyProjectile;
    private float elapsedTime; // trascorso
    private float timeLimit = 0.8f;
    [SerializeField] private int projectileCount = 0;
    private bool bombboolean = false;

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
        if (projectileCount >= 12) {
            Explode();
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Projectile")) {
            projectileCount++;
            if (projectileCount >= 12) {
                Explode();
            }
            
        } else if (other.gameObject.CompareTag("Projectile Laser")) {
            projectileCount += 2;
            if (projectileCount >= 12) {
                Explode();
            }
        }
    }

    protected override void Shoot() {

        if (playerController.isAlive) {
            audioSource.PlayOneShot(shootingSound);
            Instantiate(enemyProjectile, gameObject.transform.position, transform.rotation);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(3, 0, 0), transform.rotation);
            Instantiate(enemyProjectile, gameObject.transform.position + new Vector3(-3, 0, 0), transform.rotation);
        }
    }

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (playerController.isAlive) {
            if (timeRemaining > 3) {
                timeRemaining -= Time.deltaTime;
                transform.Translate(Vector3.forward * step);

            } else {
                timeRemaining -= Time.deltaTime;
                ShootingTime();
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
