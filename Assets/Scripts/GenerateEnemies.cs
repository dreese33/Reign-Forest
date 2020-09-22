using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{

    public static float levelOfDifficulty = 1.0f;
    private bool beingHandled = false;

    //Replace with pooling later
    public GameObject zombie;

    void Start()
    {
        Debug.Log("Working");


        StartCoroutine(SpawnZombies(20));
    }


    private IEnumerator SpawnZombies(int iters)
    {
        beingHandled = true;

        for (int i = 0; i < iters; i++)
        {
            Instantiate(zombie);
            yield return new WaitForSeconds(RandomNumberSeconds());
        }

        beingHandled = false;
    }


    private float RandomNumberSeconds()
    {
        return Random.Range(0.0f, 1.5f);
    }
}
