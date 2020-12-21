using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEnemies : MonoBehaviour
{
    //Replace with pooling later
    [SerializeField]
    GameObject zombie;

    [SerializeField]
    public Text waveLabel;

    int currentSpawnValue = 20;
    bool beingHandled = false;
    
    //Game progression values
    float minSpawnTime = 0.0f;
    float maxSpawnTime = 1.5f;
    float currentMinSpeed = 5.0f;
    float currentMaxSpeed = 15.0f;

    public int numberOfZombies = 0;
    public int wave = 0;

    //Constants
    readonly float SMALLEST_MAX_SPAWN_TIME = 0.5f;
    readonly float MAX_SPEED = 50.0f;
    readonly float MIN_SPEED = 30.0f;
    readonly float TIME_BETWEEN_WAVES = 5.0f;
    readonly int MAX_NUM_ZOMBIES = 100;

    //Pooling
    ObjectPooler objectPooler;


    float RandomNumberSeconds()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }


    float GetRandomSpeed()
    {
        return Random.Range(currentMinSpeed, currentMaxSpeed);
    }


    //Update the following functions every new wave of zombies
    void UpdateSpawnTimes()
    {
        if (maxSpawnTime > SMALLEST_MAX_SPAWN_TIME)
        {
            maxSpawnTime -= 0.1f;
        }
    }


    public IEnumerator FadeIn()
    {
        if (Statics.PlayerAlive) {
            waveLabel.text = "Wave " + wave.ToString();
            Debug.Log("Lived");
        } else {
            waveLabel.text = "You Died!";
            Debug.Log("Died");
        }

        float t = TIME_BETWEEN_WAVES / 2.0f;
        waveLabel.color = new Color(waveLabel.color.r, waveLabel.color.g, waveLabel.color.b, 0);
        waveLabel.gameObject.SetActive(true);

        while (waveLabel.color.a < 1.0f)
        {
            waveLabel.color = new Color(waveLabel.color.r, waveLabel.color.g, waveLabel.color.b, waveLabel.color.a + (Time.deltaTime / (t - 1)));
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }


    public IEnumerator FadeOut()
    {
        float t = TIME_BETWEEN_WAVES / 2.0f;
        while (waveLabel.color.a > 0.0f)
        {
            waveLabel.color = new Color(waveLabel.color.r, waveLabel.color.g, waveLabel.color.b, waveLabel.color.a - (Time.deltaTime / t));
            yield return null;
        }

        waveLabel.gameObject.SetActive(false);
    }


    public IEnumerator FadeWaveText()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
        yield return null;
    }


    void UpdateWaveValues()
    {
        wave += 1;
        StartCoroutine(FadeWaveText());
    }


    void UpdateSpeeds()
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


    void AddZombieToGame(int identifier)
    {
        GameObject newZombie = objectPooler.SpawnFromPool("Zombie"); //Instantiate(zombie);
        newZombie.name = "ZombieLowQuality" + identifier;
        
        ZombieController zombieController = newZombie.GetComponent<ZombieController>();
        zombieController.SetSpeed(GetRandomSpeed());

        numberOfZombies++;
    }


    IEnumerator SpawnZombiesDelay(int iters)
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


    void SpawnZombies(int iters)
    {
        for (int i = 0; i < iters; i++)
        {
            AddZombieToGame(i);
        }
    }


    //Called every new wave
    void StartZombieSpawner()
    {
        UpdateSpawnTimes();
        UpdateSpeeds();
        UpdateWaveValues();
        StartCoroutine(SpawnZombiesDelay(currentSpawnValue));
    }


    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine("StartZombieSpawner");
    }


    void Update() 
    {
        if (Statics.PlayerAlive)
        {
            if (numberOfZombies == 0 && !beingHandled)
            {
                StartCoroutine("StartZombieSpawner");
            }
        }
    }
}
