using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    // Three fileds: a prefab to be pooled, a queue that will hold reference to all the,
    // Game Objects and an int that will help tp pre-load the Object Pool
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Queue<GameObject> projectilePool = new Queue<GameObject>();
    private int poolStartSize = 50;


    // Instantiate copies of the prefab and adding them to the queue
    void Start() {        
        
        for (int i = 0; i < poolStartSize; i++) {
            
            GameObject projectile = Instantiate(projectilePrefab);
            projectilePool.Enqueue(projectile);
            projectile.SetActive(false);
        }
    }


    // Functions to get an object and to return an object
    public GameObject GetProjectile() {
        if (projectilePool.Count > 0) {
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        } else {
            GameObject projectile = Instantiate(projectilePrefab);
            return projectile;
        }
    }

    public void ReturnProjectile(GameObject projectile) {
        projectilePool.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
