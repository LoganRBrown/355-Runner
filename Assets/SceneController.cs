using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    public GameObject prefabPlayer;
    List<GameObject> walls;
    float delayUntilSpawn = 0;
    Vector3 playerPos = new Vector3(0, 0, 0);

	void Start () {
        Instantiate(prefabPlayer, playerPos, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

        
       
        delayUntilSpawn -= Time.deltaTime;
        if (delayUntilSpawn <= 0)
        {
            Vector3 pos = new Vector3(0, 0, 20);
            GameObject wall = Instantiate(prefabWall, pos, Quaternion.identity);
            delayUntilSpawn = Random.Range(1, 2);
            walls.Add(wall);
        }

        

        for (int I = 0; I < walls.Count; I++)
        {
            Debug.Log("here");
            if(!AABB.Collision(prefabPlayer, walls[I]))
            {
                Debug.Log("hit");
                Destroy(walls[I]);
            }
            if(walls[I].transform.position.z <= -11)
            {
                Destroy(walls[I]);
            }

        }


    }
}
