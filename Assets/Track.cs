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

    void Start()
    {
        if (wallSpawnPoints.Length == 0) return;
        if (!prefabWall) return;

        //Get a random Position
        Vector3 spawnPos = wallSpawnPoints[Random.Range(0, wallSpawnPoints.Length)].position;
        //spawn a wall, parent it to this piece of track:
        Instantiate(prefabWall, spawnPos, Quaternion.identity, transform);
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;

        if (pointOut.position.z < -20)
        {
            isDead = true;
        }
    }
}
