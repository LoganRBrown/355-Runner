using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    const float speed = -10;
    public float laneWidth = 200;
    float x = 0;
    float lane = 0;

	// Use this for initialization
	void Start () {
        lane = Random.Range(-1, 2);

        float targetX = lane * laneWidth;
        x = (targetX - transform.position.x);
        Debug.Log(lane);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(x, 0, speed) * Time.deltaTime;
	}
}
