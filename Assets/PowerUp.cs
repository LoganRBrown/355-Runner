using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType {Reverse, Push, Slide };

    public PowerUpType type;

	
	void Start () {
        type = (PowerUpType)Random.Range(0, 3);

        Renderer rend = GetComponent<Renderer>();

        switch (type)
        {
            case PowerUpType.Reverse:
                rend.material.shader = Shader.Find("_Color");
                rend.material.SetColor("_Color", Color.green);
                break;
            case PowerUpType.Push:
                rend.material.shader = Shader.Find("_Color");
                rend.material.SetColor("_Color", Color.blue);
                break;
            case PowerUpType.Slide:
                rend.material.shader = Shader.Find("_Color");
                rend.material.SetColor("_Color", Color.red);
                break;
            default:
                Debug.Log("The Power Up Type enum Broke. Check the PowerUp Script");
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
