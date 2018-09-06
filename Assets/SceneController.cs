using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    public GameObject prefabPlayer;
    float delayUntilSpawn = 0;
    Vector3 playerPos = new Vector3(0, 0, 0);

	void Start () {
        prefabPlayer = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
       
        Instantiate(prefabPlayer, playerPos, Quaternion.identity);

        delayUntilSpawn -= Time.deltaTime;
        if (delayUntilSpawn <= 0)
        {
            Vector3 pos = new Vector3(0, 0, 20);
            Instantiate(prefabWall, pos, Quaternion.identity);
            delayUntilSpawn = Random.Range(1, 3);
        }

        //Collision(prefabPlayer, prefabWall);
	}

    //private bool Collision(GameObject a,GameObject b)
    //{
    //    float minX;
    //    float maxX;
    //    float minY;
    //    float maxY;
    //    float minZ;
    //    float maxZ;

    //    if (a.minX <= b.maxX && a.maxX >= b.minX) return false;
    //    if (a.minY <= b.maxY && a.maxY >= b.minY) return false;
    //    if (a.minZ <= b.maxZ && a.maxZ >= b.minZ) return false;

    //    return true;
    //}
}
