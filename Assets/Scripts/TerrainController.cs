using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{

    //To fix bug tomorrow try to use:
    //https://answers.unity.com/questions/538769/change-and-add-terrain-texture-during-runtime.html


    public Terrain terrain;
    private Terrain frontTerrain;
    private GameObject terrainObj;

    //The total number of terrain objects rendered
    public int terrainCount = 0;
    private float terrainLength;
    private PlayerController player;

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
        terrainCount++;
        CreateNewTerrain();

        if (terrainCount > 2)
        {
            DestroyTerrain();
        }
    }


    void CreateNewTerrain()
    {
        //Set front terrain to terrain
        terrainObj = new GameObject("TerrainObj" + terrainCount);
 
        TerrainData terrainData = new TerrainData();
        
        terrainData.size = new Vector3(100, 600, 1000);
        
        TerrainCollider terrainCollider = terrainObj.AddComponent<TerrainCollider>();
        Terrain newTerrain = terrainObj.AddComponent<Terrain>();
        
        terrainCollider.terrainData = terrainData;
        newTerrain.terrainData = terrainData;
        frontTerrain = newTerrain;

        Vector3 terrainPos = terrainObj.transform.position;
        terrainPos.z = terrainCount * terrainLength;
        terrainObj.transform.position = terrainPos;
    }


    void DestroyTerrain()
    {
        Destroy(GameObject.Find("TerrainObj" + (terrainCount - 2)));
    }
}
