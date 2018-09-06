using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    const float speed = -10;
    public float laneWidth = 2;
    float X = 0;
    int lane = 0;

	// Use this for initialization
	void Start () {
        lane = Random.Range(-1, 1);

        float targetX = lane * laneWidth;
        X = (targetX - transform.position.x) * .1f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(X, 0, speed) * Time.deltaTime;
	}
}
