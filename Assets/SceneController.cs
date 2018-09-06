using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    public GameObject prefabPlayer;
    List<GameObject> walls;
    GameObject[] wallsArray;
    float delayUntilSpawn = 0;
    Vector3 playerPos = new Vector3(0, 0, 0);

	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       
        Instantiate(prefabPlayer, playerPos, Quaternion.identity);

        delayUntilSpawn -= Time.deltaTime;
        if (delayUntilSpawn <= 0)
        {
            Vector3 pos = new Vector3(0, 0, 20);
            Instantiate(prefabWall, pos, Quaternion.identity);
            walls.Add(prefabWall);
            delayUntilSpawn = Random.Range(1, 3);
        }

        wallsArray = walls.ToArray();

        for (int I = 0; I < wallsArray.Length; I++)
        {
            if (prefabPlayer) ;
        }

        AABB.Collision(prefabPlayer, prefabWall);
    }
}
