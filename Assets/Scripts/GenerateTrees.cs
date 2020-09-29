using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrees : MonoBehaviour
{

    public GameObject treeLeft;
    public GameObject treeRight;
    private GameObject[] treesRight = new GameObject[10];
    private GameObject[] treesLeft = new GameObject[10];
    
    //The index of the tree currently in front
    private int frontIndex = 0;
    private int backIndex = 0;
    private GameObject character;

    void Start()
    {
        CharacterSelect();

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

        frontIndex = 9;
    }


    void Update()
    {
        if (CameraController.PlayerAlive)
        {
            if (character.transform.position.z > treesLeft[backIndex].transform.position.z + 50.0f)
            {
                treesLeft[backIndex].transform.position = GetNewTreePosition(treesLeft[backIndex]);
                treesRight[backIndex].transform.position = GetNewTreePosition(treesRight[backIndex]);

                frontIndex = backIndex;
                if (backIndex == 9)
                {
                    backIndex = -1;
                }
                backIndex += 1;
            }
        }
    }


    private Vector3 GetNewTreePosition(GameObject tree)
    {
        Vector3 newPos = tree.transform.position;
        newPos.z += 1000.0f;
        return newPos;
    }


    private void CharacterSelect()
    {
        switch (CameraController.gender)
        {
            case CharacterType.MALE:
                character = GameObject.Find("MaleLowQuality");
                break;
            case CharacterType.FEMALE:
                character = GameObject.Find("FemaleLowFrames");
                break;
        }
    }
}
