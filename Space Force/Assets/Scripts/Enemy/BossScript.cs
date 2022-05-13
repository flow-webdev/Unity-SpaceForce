using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript {

    public GameObject enemyProjectile;
    private float elapsedTime; // trascorso
    private float timeLimit = 0.7f;

    [SerializeField] private int projectileCount = 0;
    [SerializeField] private bool bombboolean = false;
    [SerializeField] private int shootCounter = 0;
    Vector3 newPosition;
    float playerxPosition;

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

    protected override void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Projectile")) {
            projectileCount++;
            if (projectileCount >= 40) {
                Explode();
            }

        } else if (other.gameObject.CompareTag("Projectile Laser")) {
            projectileCount += 2;
            if (projectileCount >= 40) {
                Explode();
            }
        }
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
            
            playerxPosition = player.transform.position.x;
            newPosition = new Vector3(playerxPosition, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            ShootingTime();
        }
    }

    private void ShootingTime() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit) {
            elapsedTime = 0;
            Shoot();
            shootCounter++;
        }
    }

}

