using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{

    public Terrain terrain;

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
                Debug.Log("Need to add more terrain now");
            }
        }
    }
}
