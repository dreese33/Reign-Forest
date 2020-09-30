﻿using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Animation anim;
    CameraController controller;
    float speed = 0.0f;
    GameObject player;
    float rand;
    int health;
    bool rootZombie = false;

    [SerializeField]
    GameObject male;

    [SerializeField]
    GameObject female;

    //Constants
    readonly float ROTATE_SPEED = 20.0f;

    //Health bar
    [SerializeField]
    GameObject healthBar;


    void Start()
    {
        switch (PlayerController.gender)
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
                rotation.y += ROTATE_SPEED * Time.deltaTime;
            } else
            {
                rotation.y -= ROTATE_SPEED * Time.deltaTime;
            }
        }

        transform.eulerAngles = rotation;
    }


    int GetRandomHealth()
    {
        return Random.Range(50, 150);
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


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
