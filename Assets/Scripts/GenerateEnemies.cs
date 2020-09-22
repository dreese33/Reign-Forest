using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{

    public static float levelOfDifficulty = 1.0f;
    private bool beingHandled = false;

    //Replace with pooling later
    public GameObject zombie;
    public List<GameObject> zombies = new List<GameObject>();
    private int currentSpawnValue = 20;

    void Start()
    {
        Debug.Log("Working");
        StartCoroutine(SpawnZombies(currentSpawnValue));
    }


    private IEnumerator SpawnZombies(int iters)
    {
        beingHandled = true;

        for (int i = 0; i < iters; i++)
        {
            GameObject newZombie = Instantiate(zombie);
            newZombie.name = "ZombieLowQuality" + i;
            yield return new WaitForSeconds(RandomNumberSeconds());
        }

        beingHandled = false;
    }


    private float RandomNumberSeconds()
    {
        return Random.Range(0.0f, 1.5f);
    }
}
