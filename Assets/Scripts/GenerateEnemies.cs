using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    private bool beingHandled = false;

    //Replace with pooling later
    public GameObject zombie;
    private int currentSpawnValue = 20;
    public int numberOfZombies = 0;
    public int wave = 1;

    //Game progression values
    private float minSpawnTime = 0.0f;
    private float maxSpawnTime = 1.5f;
    private float currentMinSpeed = 5.0f;
    private float currentMaxSpeed = 15.0f;

    private readonly float SMALLEST_MAX_SPAWN_TIME = 0.5f;
    private readonly float MAX_SPEED = 50.0f;
    private readonly float MIN_SPEED = 30.0f;
    private readonly float TIME_BETWEEN_WAVES = 5.0f;

    void Start()
    {
        StartCoroutine("StartZombieSpawner");
    }


    void Update() 
    {
        if (CameraController.PlayerAlive)
        {
            if (numberOfZombies == 1 && !beingHandled)
            {
                StartCoroutine("StartZombieSpawner");
            }
        }
    }


    private IEnumerator SpawnZombiesDelay(int iters)
    {
        beingHandled = true;

        for (int i = 0; i < iters + 1; i++)
        {
            if (i == 0)
            {
                Debug.Log("New wave " + maxSpawnTime);
                yield return new WaitForSeconds(TIME_BETWEEN_WAVES);
                continue;
            }
            AddZombieToGame(i - 1);
            yield return new WaitForSeconds(RandomNumberSeconds());
        }

        beingHandled = false;
    }


    private void SpawnZombies(int iters)
    {
        for (int i = 0; i < iters; i++)
        {
            AddZombieToGame(i);
        }
    }


    private void AddZombieToGame(int identifier)
    {
        GameObject newZombie = Instantiate(zombie);
        newZombie.name = "ZombieLowQuality" + identifier;
        
        ZombieController zombieController = newZombie.GetComponent<ZombieController>();
        zombieController.SetSpeed(GetRandomSpeed());

        numberOfZombies++;
    }


    private float RandomNumberSeconds()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }


    private void StartZombieSpawner()
    {
        UpdateSpawnTimes();
        UpdateSpeeds();
        UpdateWaveValues();
        StartCoroutine(SpawnZombiesDelay(currentSpawnValue));
    }


    float GetRandomSpeed()
    {
        return Random.Range(currentMinSpeed, currentMaxSpeed);
    }


    //Update the following functions every new wave of zombies
    private void UpdateSpawnTimes()
    {
        if (maxSpawnTime > SMALLEST_MAX_SPAWN_TIME)
        {
            maxSpawnTime -= 0.1f;
        }
    }


    private void UpdateWaveValues()
    {
        wave += 1;
    }


    private void UpdateSpeeds()
    {
        if (currentMaxSpeed <= MAX_SPEED)
        {
            currentMaxSpeed += 1.0f;
        }

        if (currentMinSpeed <= MIN_SPEED)
        {
            currentMinSpeed += 1.0f;
        }
    }
}
