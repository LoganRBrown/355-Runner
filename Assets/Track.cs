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

    Vector3 wallSpawnPos = new Vector3(0,0,0);

    void Start()
    {
        if (wallSpawnPoints.Length == 0) return;
        if (!prefabWall) return;

        //Get a random Position
        Vector3 spawnPos = wallSpawnPoints[Random.Range(0, wallSpawnPoints.Length)].position;
        wallSpawnPos = spawnPos;
        //spawn a wall, parent it to this piece of track:
        Instantiate(prefabWall, spawnPos, Quaternion.identity, transform);

        GameObject cam = GameObject.Find("Main Camera");
        SceneController control = cam.GetComponent<SceneController>();

        if (control.powerUpTimer <= 0)
        {
            Vector3 powerUpSpawn = wallSpawnPoints[Random.Range(0, wallSpawnPoints.Length)].position;
            if (powerUpSpawn != wallSpawnPos) Instantiate(prefabPowerUp, powerUpSpawn, Quaternion.identity, transform);
            prefabPowerUp.transform.localScale = new Vector3(.5f, 1.5f, .5f);
            prefabPowerUp.transform.position = new Vector3(0, .3f, 0);
            control.powerUpTimer = 3;
        }

        //Debug.Log(prefabPowerUp.type);

    }

    void Update()
    {
        //Debug.Log(timer);
        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;

        if (pointOut.position.z < -20)
        {
            isDead = true;
        }
    }
}
