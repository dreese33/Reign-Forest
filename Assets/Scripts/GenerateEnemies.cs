using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{

    public static float levelOfDifficulty = 1.0f;
    private bool beingHandled = false;

    //Replace with pooling later
    public GameObject zombie;
    private int currentSpawnValue = 20;
    public int numberOfZombies = 0;
    private float minSpawnTime = 0.0f;
    private float maxSpawnTime = 1.5f;

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
                yield return new WaitForSeconds(10.0f);
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
        numberOfZombies++;
    }


    private float RandomNumberSeconds()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }


    private void StartZombieSpawner()
    {
        UpdateSpawnTimes();
        StartCoroutine(SpawnZombiesDelay(currentSpawnValue));
    }


    //Update this every new wave of zombies
    private void UpdateSpawnTimes()
    {
        if (maxSpawnTime > 0.5)
        {
            maxSpawnTime -= 0.1f;
        }
    }


    public int GetWaveNumber()
    {
        return (int) levelOfDifficulty;
    }
}
