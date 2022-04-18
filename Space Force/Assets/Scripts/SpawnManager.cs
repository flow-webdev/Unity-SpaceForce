using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject powerUp;
    public GameObject[] enemy;
    private Vector3[] enemySpawnCoordinates;

    int previousCoordinate = 0;

    private float spawnPosY = 10f;
    private float spawnPosZ = 25f;

    private float startDelayAsteroid = 2f;
    private float asteroidsRate = 3f;

    private float startDelayPowerup = 15f;    
    private float powerupRate = 30f;

    void Start() {

        InitializeEnemyCoordinates();

        //InvokeRepeating("AsteroidsSpawn", startDelayAsteroid, asteroidsRate);
        InvokeRepeating("PowerUpSpawn", startDelayPowerup, powerupRate);
        InvokeRepeating("EnemySpawn", startDelayAsteroid, asteroidsRate);
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

    void EnemySpawn() {

        int enemyIndex = Random.Range(0, enemy.Length);

        //Debug.Log("Previous: " + previousCoordinate);        
        int coordinatesIndex = ReturnCoordinates(previousCoordinate);
        previousCoordinate = coordinatesIndex;
        //Debug.Log("Actual: " + coordinatesIndex);

        Instantiate(enemy[enemyIndex], enemySpawnCoordinates[coordinatesIndex], enemy[enemyIndex].gameObject.transform.rotation);
    }    

    void InitializeEnemyCoordinates() {
        enemySpawnCoordinates = new Vector3[9];
        enemySpawnCoordinates[0] = new Vector3(-20, 10, 34);
        enemySpawnCoordinates[1] = new Vector3(-15, 10, 34);
        enemySpawnCoordinates[2] = new Vector3(-10, 10, 34);
        enemySpawnCoordinates[3] = new Vector3(-5, 10, 34);
        enemySpawnCoordinates[4] = new Vector3(0, 10, 34);
        enemySpawnCoordinates[5] = new Vector3(5, 10, 34);
        enemySpawnCoordinates[6] = new Vector3(10, 10, 34);
        enemySpawnCoordinates[7] = new Vector3(15, 10, 34);
        enemySpawnCoordinates[8] = new Vector3(20, 10, 34);
    }

    int ReturnCoordinates(int coordinate) {

        int coordinatesIndex = coordinate;

        while (coordinatesIndex == coordinate) {
            coordinatesIndex = Random.Range(0, enemySpawnCoordinates.Length);
        }

        return coordinatesIndex;
    }

    //Vector3 PowerUpRandomVector() {
    //    float randomX = Random.Range(-spawnPosY, spawnPosY);
    //    float randomZ = Random.Range(startDelay, spawnPosZPowerUp);
    //    return new Vector3(randomX, spawnPosY, randomZ);
    //}
}
