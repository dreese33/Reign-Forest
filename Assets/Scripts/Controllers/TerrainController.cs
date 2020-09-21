using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{

    //Add texture to Terrain
    //https://answers.unity.com/questions/538769/change-and-add-terrain-texture-during-runtime.html


    public Terrain terrain;
    private static Terrain terrainObj;

    //The total number of terrain objects rendered
    public int terrainCount = 0;
    private float terrainLength;
    private PlayerController player;
    private static bool firstIter = true;

    // Start is called before the first frame update
    void Start()
    {
        terrain.allowAutoConnect = true;
        player = GameObject.Find("ForwardButton").GetComponent<PlayerController>();
        terrainLength = terrain.terrainData.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.forwardPressed)
        {
            float playerPosition = GetPlayerPosition();
            
            if (playerPosition > 0)
            {
                AddNewTerrain();
            }
        }
    }


    float GetPlayerPosition()
    {
        return player.character.transform.position.z - terrainCount * terrainLength;
    }


    void AddNewTerrain()
    {
        Debug.Log("Add new terrain");
        terrainCount++;

        CreateNewTerrain();
    }


    void CreateNewTerrain()
    {
        if (firstIter)
        {
            firstIter = false;
            terrainObj = Instantiate(terrain, new Vector3(0, 0, 1000), Quaternion.identity);
            terrainObj.name = "TerrainObj" + terrainCount;
            Debug.Log("Another one created");
        } else
        {
            if (terrainCount % 2 == 0)
            {
                Debug.Log("Moved terrain");
                Terrain terrainNow = GameObject.Find("Terrain").GetComponent<Terrain>();
                Vector3 terrainPos = terrainNow.transform.position;
                terrainPos.z = terrainCount * terrainLength;
                terrainNow.transform.position = terrainPos;
            } else
            {
                Debug.Log("Moved obj");
                Terrain terrainNow = GameObject.Find("TerrainObj1").GetComponent<Terrain>();
                Vector3 terrainPos = terrainNow.transform.position;
                terrainPos.z = terrainCount * terrainLength;
                terrainNow.transform.position = terrainPos;
            }
        }
    }


    void DestroyTerrain()
    {
        Destroy(GameObject.Find("TerrainObj" + (terrainCount - 2)));
    }
}
