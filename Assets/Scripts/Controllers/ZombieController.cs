using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animation anim;
    private CameraController controller;
    private float speed = 0.0f;
    private readonly float rotateSpeed = 20.0f;
    public GameObject male;
    public GameObject female;
    private GameObject player;
    private float rand;
    private int health;
    private bool rootZombie = false;

    //Health bar
    public GameObject healthBar;


    void Start()
    {
        switch (CameraController.gender)
        {
            case CharacterType.MALE:
                player = male;
                break;
            case CharacterType.FEMALE:
                player = female;
                break;
        }

        anim = gameObject.GetComponent<Animation>();
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        transform.position = GetRandomPosition();
        speed = GetRandomSpeed();
        health = GetRandomHealth();

        if (name == "ZombieLowQuality")
        {
            //Out of sight out of mind
            transform.position = new Vector3(-100.0f, transform.position.y, -100.0f);
            rootZombie = true;
        }
    }


    void Update()
    {
        if (!rootZombie)
        {
            anim.Play("Zombie|Walk");

            if (PastPlayer())
            {
                CameraController.PlayerAlive = false;
            } else
            {
                transform.position += speed * transform.forward * Time.deltaTime;
            }

            if (InRadius())
            {
                RotateZombiePlayer();
            }
        }
    }


    Vector3 GetRandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range(12.5f, 82.5f);

        rand = Random.Range(300.0f, 850.0f);
        pos.z = player.transform.position.z + rand;
        return pos;
    }


    float GetRandomSpeed()
    {
        return Random.Range(5.0f, 20.0f * GenerateEnemies.levelOfDifficulty);
    }
    

    bool InRadius()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 100.0f;
    }


    bool PastPlayer()
    {
        return (transform.position.z - player.transform.position.z) < 15.0f;
    }


    void RotateZombiePlayer()
    {
        //Possibly needs optimization (many zombies will spawn)
        Vector3 rotation = transform.eulerAngles;
        float deltaZ = transform.position.z - player.transform.position.z;
        float deltaX = transform.position.x - player.transform.position.x;
        float optimalAngle = Mathf.Atan(deltaX / deltaZ) * Mathf.Rad2Deg + 180.0f;

        if (Mathf.Abs(transform.eulerAngles.y - optimalAngle) > 1.0f) 
        {
            if (transform.eulerAngles.y < optimalAngle)
            {
                rotation.y += rotateSpeed * Time.deltaTime;
            } else
            {
                rotation.y -= rotateSpeed * Time.deltaTime;
            }
        }

        transform.eulerAngles = rotation;
    }


    public void SubtractFromHealth(int damage)
    {
        health -= damage;
        //Update health bar
    }


    public int GetHealth()  
    {
        return health;
    }


    private int GetRandomHealth()
    {
        return Random.Range(50, 150);
    }
}
