using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrees : MonoBehaviour
{

    public GameObject treeLeft;
    public GameObject treeRight;
    private GameObject[] treesRight = new GameObject[10];
    private GameObject[] treesLeft = new GameObject[10];
    private CameraController controller;
    
    //The index of the tree currently in front
    private int frontIndex = 0;

    void Start()
    {
        Vector3 newPosLeft, newPosRight;
        newPosLeft = new Vector3(20.0f, 4.5f, 40.0f);
        newPosRight = new Vector3(80.0f, 4.5f, 40.0f);

        for (int i = 0; i < 10; i++)
        {
            newPosLeft.z += 100.0f;
            newPosRight.z += 100.0f;

            treesLeft[i] = Instantiate(treeLeft, newPosLeft, Quaternion.identity);
            treesRight[i] = Instantiate(treeRight, newPosRight, Quaternion.identity);
        }

        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }


    void Update()
    {
        
    }
}
