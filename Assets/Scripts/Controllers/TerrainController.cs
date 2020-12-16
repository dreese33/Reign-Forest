using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    Terrain terrain;

    //The total number of terrain objects rendered
    public int terrainCount = 0;

    float terrainLength;
    PlayerController player;


    float GetPlayerPosition()
    {
        return player.character.transform.position.z - terrainCount * terrainLength;
    }


    void AddTerrain()
    {
        Statics.TerrainFirstIter = false;
        Statics.TerrainObj = Instantiate(terrain, new Vector3(0, 0, 1000), Quaternion.identity);
        Statics.TerrainObj.name = "TerrainObj" + terrainCount;
        Debug.Log("Another one created");
    }


    void MoveTerrain(string terrainName)
    {
        Terrain terrainNow = GameObject.Find(terrainName).GetComponent<Terrain>();
        Vector3 terrainPos = terrainNow.transform.position;
        terrainPos.z = terrainCount * terrainLength;
        terrainNow.transform.position = terrainPos;
    }


    //This is essentially pooled, so this function is never really called
    void DestroyTerrain()
    {
        Destroy(GameObject.Find("TerrainObj" + (terrainCount - 2)));
    }


    void CreateNewTerrain()
    {
        if (Statics.TerrainFirstIter)
        {
            AddTerrain();
        } else
        {
            if (terrainCount % 2 == 0)
            {
                Debug.Log("Moved terrain");
                MoveTerrain("Terrain");
            } else
            {
                Debug.Log("Moved obj");
                MoveTerrain("TerrainObj1");
            }
        }
    }


    void AddNewTerrain()
    {
        Debug.Log("Add new terrain");
        terrainCount++;
        CreateNewTerrain();
    }


    void Start()
    {
        terrain.allowAutoConnect = true;
        player = GameObject.Find("ForwardButton").GetComponent<PlayerController>();
        terrainLength = terrain.terrainData.size.z;
    }
    

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
}
