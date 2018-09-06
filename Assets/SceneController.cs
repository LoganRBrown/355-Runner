using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    float delayUntilSpawn = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        delayUntilSpawn -= Time.deltaTime;
        if (delayUntilSpawn <= 0)
        {
            Vector3 pos = new Vector3(0, 0, 20);
            Instantiate(prefabWall, pos, Quaternion.identity);
            delayUntilSpawn = Random.Range(1, 3);
        }

        Collision(Player, prefabWall);
	}

    bool Collision(Player, prefabWall)
    {
        float minX;
        float maxX;
        float minY;
        float maxY;
        float minZ;
        float maxZ;

        if (a.minX <= b.maxX && a.maxX >= b.minX) return false;
        if (a.minY <= b.maxY && a.maxY >= b.minY) return false;
        if (a.minZ <= b.maxZ && a.maxZ >= b.minZ) return false;

        return true;
    }
}
