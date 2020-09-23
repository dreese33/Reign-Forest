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

    void Start()
    {
        StartZombieSpawner();
    }


    void Update() 
    {
        if (CameraController.PlayerAlive)
        {
            if (numberOfZombies == 1 && !beingHandled)
            {
                StartZombieSpawner();
            }
        }
    }


    private IEnumerator SpawnZombiesDelay(int iters, float delay)
    {
        beingHandled = true;

        for (int i = 0; i < iters; i++)
        {
            AddZombieToGame(i);
            yield return new WaitForSeconds(delay);
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
        return Random.Range(0.0f, 1.5f);
    }


    private void StartZombieSpawner()
    {
        SpawnZombies(1);
        StartCoroutine(SpawnZombiesDelay(currentSpawnValue, RandomNumberSeconds()));
    }
}
