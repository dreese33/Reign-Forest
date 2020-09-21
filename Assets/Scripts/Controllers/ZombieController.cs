using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animation anim;
    private CameraController controller;
    private float speed = 20.0f;
    public GameObject male;
    public GameObject female;


    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        transform.position = GetRandomPosition();
    }


    void Update()
    {
        if (controller.PlayerAlive)
        {
            anim.Play("Zombie|Walk");
            Vector3 pos = transform.position;
            pos.z -= speed * Time.deltaTime;
            transform.position = pos;
        }
    }


    Vector3 GetRandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range(12.5f, 82.5f);

        float rand = Random.Range(300.0f, 850.0f);
        switch (CameraController.gender) {
            case CharacterType.MALE:
                pos.z = male.transform.position.z + rand;
                break;
            case CharacterType.FEMALE:
                pos.z = female.transform.position.z + rand;
                break;
        }

        return pos;
    }
}
