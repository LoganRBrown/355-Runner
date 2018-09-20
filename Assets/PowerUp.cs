using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [HideInInspector] public enum PowerUpType {Swap, Push, Slide };

    public PowerUpType type;

    public Material[] material;

    Renderer rend;

	
	void Awake () {
        type = (PowerUpType)Random.Range(0, 3);

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

        switch (type)
        {
            case PowerUpType.Swap:
                rend.sharedMaterial = material[1];
                break;
            case PowerUpType.Push:
                rend.sharedMaterial = material[2];
                break;
            case PowerUpType.Slide:
                rend.sharedMaterial = material[3];
                break;
            default:
                Debug.Log("The Power Up Type enum Broke. Check the PowerUp Script");
                break;
        }
	}
}
