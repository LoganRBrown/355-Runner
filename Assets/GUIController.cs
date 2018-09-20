using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneController))]

public class GUIController : MonoBehaviour {

    public Text scoreText;
    SceneController scene;

	void Start () {
        scene = GetComponent<SceneController>();
	}
	
	// Update is called once per frame
	void Update () {
        //scoreText.text = "Score: " + scene.score;
	}
}
