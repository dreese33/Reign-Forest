using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{

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
        /*if (frontTerrain == null)
        {
            terrainCount++;
            CreateNewTerrain();
            return;
        }*/

        terrainCount++;
        CreateNewTerrain();

        if (terrainCount > 2)
        {
            DestroyTerrain();
        }

        //Questionable code here
        /*
        if (terrainCount > 1)
        {
            Destroy(frontTerrain);
            terrain.terrainData = frontTerrain.terrainData;
            frontTerrain = null;
        } else
        {
            terrain.terrainData = frontTerrain.terrainData;
            frontTerrain = null;
        }*/
    }


    void CreateNewTerrain()
    {
        //Set front terrain to terrain
        terrainObj = new GameObject("TerrainObj" + terrainCount);
 
        TerrainData _TerrainData = new TerrainData();
        
        _TerrainData.size = new Vector3(100, 600, 1000);
        //_TerrainData.heightmapResolution = 100;
        //_TerrainData.baseMapResolution = 100;
       // _TerrainData.SetDetailResolution(100, 16);
        
        TerrainCollider terrainCollider = terrainObj.AddComponent<TerrainCollider>();
        Terrain newTerrain = terrainObj.AddComponent<Terrain>();
        
        terrainCollider.terrainData = _TerrainData;
        newTerrain.terrainData = _TerrainData;
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
