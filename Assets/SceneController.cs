﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    public GameObject prefabPlayer;
    public Track prefabTrack;
    List<GameObject> walls = new List<GameObject>();
    float delayUntilSpawn = 0;
    Vector3 playerPos = new Vector3(0, 0, 0);

    List<Track> tracks = new List<Track>();
    //public [] prefabTracks For using different track prefabs

	void Start () {
        Instantiate(prefabPlayer, playerPos, Quaternion.identity);

        SpawnTrack();

    }
	
	// Update is called once per frame
	void Update () {

        for(int i = tracks.Count - 1; i >=0; i--)
        {
            if (tracks[i].isDead)
            {
                Destroy(tracks[i].gameObject);
                tracks.RemoveAt(i);
            }
        }

        if (tracks.Count < 5) SpawnTrack();
    }

    void SpawnTrack()
    {
        while (tracks.Count < 5)
        {
            Vector3 ptOut = new Vector3(0, -.5f, -10);
            if(tracks.Count > 0)ptOut = tracks[tracks.Count - 1].pointOut.position;

            //Track prefab = prefabTrack[Random.Range(0, prefabTrack.Length)]; Spawning random dofferent prefab tracks

            Vector3 ptIn = prefabTrack.pointIn.position;

            Vector3 pos = (prefabTrack.transform.position - ptIn) + ptOut;

            Track newTrack = Instantiate(prefabTrack, pos, Quaternion.identity);
            tracks.Add(newTrack);
        }
    }
}
