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
    }


    void Update()
    {
        if (controller.PlayerAlive)
        {
            anim.Play("Zombie|Walk");
            Vector3 pos = transform.position;

            //Possibly optimize later
            float subVal = speed * Time.deltaTime;
            pos.z -= subVal;

            transform.position = pos;

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


    void RotateZombiePlayer()
    {
        //Needs optimization (many zombies will spawn)
        Vector3 rotation = transform.eulerAngles;
        float deltaZ = transform.position.z - player.transform.position.z;
        float deltaX = transform.position.x - player.transform.position.x;
        float optimalAngle = Mathf.Atan(deltaX / deltaZ) * Mathf.Rad2Deg + 180.0f;
        Debug.Log("Optimal angle " +  optimalAngle);

        if (Mathf.Abs(transform.eulerAngles.y - optimalAngle) > 2.0f) 
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
}
