using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaserPool : MonoBehaviour {

    // Three fileds: a prefab to be pooled, a queue that will hold reference to all the,
    // Game Objects and an int that will help tp pre-load the Object Pool
    public GameObject projectileLaserPrefab;
    public Queue<GameObject> projectilePoolLaser = new Queue<GameObject>();
    private int poolStartSize = 40;


    // Instantiate copies of the prefab and adding them to the queue
    void Start() {

        for (int i = 0; i < poolStartSize; i++) {

            GameObject projectile = Instantiate(projectileLaserPrefab);
            projectilePoolLaser.Enqueue(projectile);
            projectile.SetActive(false);
        }
    }


    // Functions to get an object and to return an object
    public GameObject GetProjectile() {
        if (projectilePoolLaser.Count > 0) {
            GameObject projectile = projectilePoolLaser.Dequeue();
            projectile.SetActive(true);
            return projectile;
        } else {
            GameObject projectile = Instantiate(projectileLaserPrefab);
            return projectile;
        }
    }

    public void ReturnProjectile(GameObject projectile) {
        projectilePoolLaser.Enqueue(projectile);
        projectile.SetActive(false);
    }

}
