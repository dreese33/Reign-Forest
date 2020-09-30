using UnityEngine;

public class GenerateTrees : MonoBehaviour
{
    [SerializeField]
    GameObject treeLeft;

    [SerializeField]
    GameObject treeRight;

    GameObject[] treesRight = new GameObject[10];
    GameObject[] treesLeft = new GameObject[10];
    
    //The index of the tree currently in front
    int frontIndex = 0;
    int backIndex = 0;
    GameObject character;
    float[] rightRange = new float[] {75.0f, 85.0f};
    float[] leftRange = new float[] {15.0f, 25.0f};

    void Start()
    {
        CharacterSelect();

        Vector3 newPosLeft, newPosRight;
        newPosLeft = new Vector3(20.0f, 4.5f, 40.0f);
        newPosRight = new Vector3(80.0f, 4.5f, 40.0f);

        GenerateInitialTrees(newPosLeft, newPosRight);

        frontIndex = 9;
    }


    void GenerateInitialTrees(Vector3 newPosLeft, Vector3 newPosRight)
    {
        for (int i = 0; i < 10; i++)
        {
            newPosLeft.z += 100.0f;
            newPosRight.z += 100.0f;

            newPosLeft.x = Random.Range(leftRange[0], leftRange[1]);
            newPosRight.x = Random.Range(rightRange[0], rightRange[1]);

            treesLeft[i] = Instantiate(treeLeft, newPosLeft, Quaternion.identity);
            treesRight[i] = Instantiate(treeRight, newPosRight, Quaternion.identity);
        }
    }


    void Update()
    {
        if (CameraController.PlayerAlive)
        {
            if (character.transform.position.z > treesLeft[backIndex].transform.position.z + 50.0f)
            {
                treesLeft[backIndex].transform.position = GetNextPosition(treesLeft[backIndex], leftRange);
                treesRight[backIndex].transform.position = GetNextPosition(treesRight[backIndex], rightRange);

                frontIndex = backIndex;
                if (backIndex == 9)
                {
                    backIndex = -1;
                }
                backIndex += 1;
            }
        }
    }



    //range can only be of length 2, containing two floats
    Vector3 GetNextPosition(GameObject tree, float[] range)
    {
        Vector3 newPos = tree.transform.position;
        newPos.z += 1000.0f;
        newPos.x = Random.Range(range[0], range[1]);
        return newPos;
    }


    void CharacterSelect()
    {
        switch (PlayerController.gender)
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
