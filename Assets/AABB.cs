using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    float minX;
    float maxX;
    float minY;
    float maxY;
    float minZ;
    float maxZ;

    public float halfW;
    public float halfH;
    public float halfL;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void setSize(float w, float h, float l)
    //{
    //    halfW = w / 2;
    //    halfH = h / 2;
    //    halfL = l / 2;
    //}

    public void calcEdges(float x, float y, float z)
    {
        minX = x - halfW;
        maxX = x + halfW;
        minY = y - halfH;
        maxY = y + halfH;
        minZ = z - halfL;
        maxZ = z + halfL;
    }

    public bool Collision(GameObject a, GameObject b)
    {

        if (a.minX <= b.maxX && a.maxX >= b.minX) return false;
        if (a.minY <= b.maxY && a.maxY >= b.minY) return false;
        if (a.minZ <= b.maxZ && a.maxZ >= b.minZ) return false;

        return true;
    }

    public Vector3 findFix()
    {
        Vector3 solution = new Vector3();

        return solution;
    }
}
