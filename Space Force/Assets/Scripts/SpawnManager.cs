using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject powerUp;    
    private float spawnPosY = 10f;
    private float spawnPosZ = 25f;
    //private float spawnPosZPowerUp = 7f;
    private float startDelayAsteroid = 2f;
    private float asteroidsRate = 3f;
    private float startDelayPowerup = 15f;    
    private float powerupRate = 30f;

    void Start() {

        InvokeRepeating("AsteroidsSpawn", startDelayAsteroid, asteroidsRate);
        InvokeRepeating("PowerUpSpawn", startDelayPowerup, powerupRate);        
    }

    void AsteroidsSpawn() {

        int index = Random.Range(0, asteroids.Length);                

        Instantiate(asteroids[index], RandomVector(), asteroids[index].gameObject.transform.rotation); 
    }

    void PowerUpSpawn() {

        Instantiate(powerUp, RandomVector(), powerUp.gameObject.transform.rotation);
    }

    Vector3 RandomVector() {

        float randomX = Random.Range(-spawnPosY, spawnPosY);
        return new Vector3(randomX, spawnPosY, spawnPosZ);
    }

    //Vector3 PowerUpRandomVector() {
    //    float randomX = Random.Range(-spawnPosY, spawnPosY);
    //    float randomZ = Random.Range(startDelay, spawnPosZPowerUp);
    //    return new Vector3(randomX, spawnPosY, randomZ);
    //}
}
