using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject[] powerUps;
    public GameObject[] enemies;
    public GameObject runner;
    private Vector3[] enemySpawnCoordinates;
    private bool superBool = false;

    int previousCoordinate = 0;

    private float spawnPosY = 10f;
    private float spawnPosZ = 25f;

    private float startDelayEnemy = 2f;
    private float enemyRate = 3f;

    private float startDelayPowerup = 15f;    
    private float powerupRate = 30f;

    private float startDelayRunner = 15f;
    private float runnerRate = 15f;

    private void Start() {

        InitializeEnemyCoordinates();

        //InvokeRepeating("AsteroidsSpawn", startDelayAsteroid, asteroidsRate);
        InvokeRepeating("PowerUpSpawn", startDelayPowerup, powerupRate);
        InvokeRepeating("EnemySpawn", startDelayEnemy, enemyRate);
        InvokeRepeating("RunnerSpawn", startDelayRunner, runnerRate);
    }

    private void Update() {
        if (GameManager.Instance.isVictory) {
            CancelInvoke();
        }

        if (GameManager.Instance.time < 100 && !superBool) {
            CancelInvoke("EnemySpawn");
            InvokeRepeating("EnemySpawn", 0f, 1.5f);
            superBool = true;
        }
    }

    void AsteroidsSpawn() {
        int index = Random.Range(0, asteroids.Length);
        Instantiate(asteroids[index], RandomVector(), asteroids[index].gameObject.transform.rotation); 
    }

    void PowerUpSpawn() {
        int index = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[index], RandomVector(), powerUps[index].gameObject.transform.rotation);
    }

    Vector3 RandomVector() {

        float randomX = Random.Range(-spawnPosY, spawnPosY);
        return new Vector3(randomX, spawnPosY, spawnPosZ);
    }

    void EnemySpawn() {

        int enemyIndex = Random.Range(0, enemies.Length);
      
        int coordinatesIndex = ReturnCoordinates(previousCoordinate);
        previousCoordinate = coordinatesIndex;

        Instantiate(enemies[enemyIndex], enemySpawnCoordinates[coordinatesIndex], enemies[enemyIndex].gameObject.transform.rotation);
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

    void RunnerSpawn() {

        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(-42, 10, 34);
        points[1] = new Vector3(42, 10, 34);

        int index = Random.Range(0, points.Length);

        Instantiate(runner, points[index], runner.transform.rotation);
    }

    //Vector3 PowerUpRandomVector() {
    //    float randomX = Random.Range(-spawnPosY, spawnPosY);
    //    float randomZ = Random.Range(startDelay, spawnPosZPowerUp);
    //    return new Vector3(randomX, spawnPosY, randomZ);
    //}
}
