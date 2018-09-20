using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabWall;
    public GameObject prefabPlayerOne;
    public GameObject prefabPlayerTwo;
    public Track prefabTrack;
    //float delayUntilSpawn = 0;
    Vector3 playerOnePos = new Vector3(-7, 10, 0);
    Vector3 playerTwoPos = new Vector3(7, 10, 0);

    List<Track> tracks = new List<Track>();
    //public [] prefabTracks For using different track prefabs

    [HideInInspector] public bool playerIsDead = false;

    [HideInInspector] public int score = 0;

    public float powerUpTimer = 2;

	void Start () {
        Instantiate(prefabPlayerOne, playerOnePos, Quaternion.identity);

        Instantiate(prefabPlayerTwo, playerTwoPos, Quaternion.identity);

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

        if (tracks.Count < 10) SpawnTrack();

        powerUpTimer -= 1 * Time.deltaTime;

        CheckGameOver();
    }

    void SpawnTrack()
    {
        while (tracks.Count < 10)
        {
            Vector3 ptOut = new Vector3(0, -.5f, -10);
            if(tracks.Count > 0)ptOut = tracks[tracks.Count - 1].pointOut.position;

            Vector3 ptIn = prefabTrack.pointIn.position;

            Vector3 pos = (prefabTrack.transform.position - ptIn) + ptOut;

            Track newTrack = Instantiate(prefabTrack, pos, Quaternion.identity);
            tracks.Add(newTrack);
        }
    }

    void CheckGameOver()
    {
        PlayerMovement playerOne = prefabPlayerOne.GetComponent<PlayerMovement>();

        PlayerTwoMovement playerTwo = prefabPlayerTwo.GetComponent<PlayerTwoMovement>();

        if (playerOne.playerOneIsDead || playerTwo.playerTwoIsDead)
        {
            
            for(int i = tracks.Count - 1; i>=0; i--)
            {
                Destroy(tracks[i].gameObject);
                tracks.RemoveAt(i);
            }

            Destroy(prefabPlayerOne.gameObject);

            Destroy(prefabPlayerTwo.gameObject);

            Application.Quit();
        }
    }
}
