using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{

    //To fix bug tomorrow try to use:
    //https://answers.unity.com/questions/538769/change-and-add-terrain-texture-during-runtime.html


    public Terrain terrain;
    private Terrain frontTerrain;
    private static Terrain terrainObj;

    //The total number of terrain objects rendered
    public int terrainCount = 0;
    private float terrainLength;
    private PlayerController player;
    private bool lastSelected = false;
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
            float playerPosition = player.character.transform.position.z - terrainCount * terrainLength;
            if (playerPosition > 50)
            {
                AddNewTerrain();
            }
        }
    }


    void AddNewTerrain()
    {
        Debug.Log("Add new terrain");
        terrainCount++;
        if (lastSelected) {
            lastSelected = false;
        } else {
            lastSelected = true;
        }

        CreateNewTerrain();

        /*if (terrainCount > 2)
        {
            DestroyTerrain();
        }*/
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
        {/*
            if (lastSelected)
            {
                //Move terrain to front
                Debug.Log("Try");
                Vector3 terrainPos = terrain.transform.position;
                Debug.Log(terrainPos.z);
                Debug.Log(terrainObj.transform.position.z);
                if (terrainCount % 2 != 0)
                {
                    terrainPos.z = terrainCount * terrainLength;
                    terrain.transform.position = terrainPos;
                    Debug.Log("Last selected");
                } else if (terrainCount == 2)
                {
                    Debug.Log("Error prone zone");
                    terrainPos.z = terrainCount * terrainLength;
                }
            } else
            {
                //Move terrainObj to front
                //if (terrainObj != null)
                //{
                    Debug.Log("Try2");
                    Vector3 terrainPos = terrainObj.transform.position;
                    Debug.Log(terrainPos.z);
                    Debug.Log(terrain.transform.position.z);
                    Debug.Log("COunt" + terrainCount);
                    if (terrainCount % 2 == 0 && terrainCount != 2)
                    {
                        terrainPos.z = terrainCount * terrainLength;
                        Debug.Log("Else");
                    }

                    terrainObj.transform.position = terrainPos;
                //}*/

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
        //Set front terrain to terrain
        /*
        terrainObj = new GameObject("TerrainObj" + terrainCount);
 
        TerrainData terrainData = new TerrainData();
        terrainData.size = new Vector3(100, 600, 1000);
        
        //TerrainLayer[] terrainLayers = new TerrainLayer[1];

        terrainData.terrainLayers = terrain.terrainData.terrainLayers;

        TerrainCollider terrainCollider = terrainObj.AddComponent<TerrainCollider>();
        Terrain newTerrain = terrainObj.AddComponent<Terrain>();
        
        terrainCollider.terrainData = terrainData;
        newTerrain.terrainData = terrainData;
        frontTerrain = newTerrain;

        Vector3 terrainPos = terrainObj.transform.position;
        terrainPos.z = terrainCount * terrainLength;
        terrainObj.transform.position = terrainPos;*/
    }


    void DestroyTerrain()
    {
        Destroy(GameObject.Find("TerrainObj" + (terrainCount - 2)));
    }
}
