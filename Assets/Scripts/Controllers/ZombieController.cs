using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animation anim;
    private CameraController controller;
    private float speed = 0.0f;
    public GameObject male;
    public GameObject female;
    private float rand;


    void Start()
    {
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

            switch (CameraController.gender)
            {
                case CharacterType.MALE:
                    if (InMaleRadius())
                    {
                        //Turn toward male
                        Debug.Log("male turn");
                    }
                    break;
                case CharacterType.FEMALE:
                    if (InFemaleRadius())
                    {
                        //Turn toward female
                        Debug.Log("female turn");
                    }
                    break;
            }
        }
    }


    Vector3 GetRandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range(12.5f, 82.5f);

        rand = Random.Range(300.0f, 850.0f);
        switch (CameraController.gender)
        {
            case CharacterType.MALE:
                pos.z = male.transform.position.z + rand;
                break;
            case CharacterType.FEMALE:
                pos.z = female.transform.position.z + rand;
                break;
        }

        return pos;
    }


    float GetRandomSpeed()
    {
        return Random.Range(5.0f, 20.0f * GenerateEnemies.levelOfDifficulty);
    }
    

    bool InMaleRadius()
    {
        return Vector3.Distance(transform.position, male.transform.position) < 100.0f;
    }


    bool InFemaleRadius()
    {
        return Vector3.Distance(transform.position, female.transform.position) < 100.0f;
    }
}
