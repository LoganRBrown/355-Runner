using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

    public Transform pointIn;
    public Transform pointOut;

    public float speed = 10;

    [HideInInspector] public bool isDead = false;

    public Transform[] wallSpawnPoints;

    public GameObject prefabWall;

    public PowerUp prefabPowerUp;

    void Start()
    {
        if (wallSpawnPoints.Length == 0) return;
        if (!prefabWall) return;

        //Get a random Position
        Vector3 spawnPos = wallSpawnPoints[Random.Range(0, wallSpawnPoints.Length)].position;
        //spawn a wall, parent it to this piece of track:
        Instantiate(prefabWall, spawnPos, Quaternion.identity, transform);

        Vector3 powerUpSpawn = wallSpawnPoints[Random.Range(0, wallSpawnPoints.Length)].position;
        if(powerUpSpawn != spawnPos) Instantiate(prefabPowerUp, powerUpSpawn, Quaternion.identity, transform);

        //Debug.Log(prefabPowerUp.type);
        
    }

    void Update()
    {
        prefabPowerUp.transform.localScale = new Vector3(1, 1, 1);
        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;

        if (pointOut.position.z < -20)
        {
            isDead = true;
        }
    }
}
