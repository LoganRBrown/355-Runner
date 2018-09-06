using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    public static bool Collision(GameObject a, GameObject b)
    {
        float aMinX = a.transform.position.x - (a.transform.localScale.x / 2);
        float aMaxX = a.transform.position.x + (a.transform.localScale.x / 2);
        float aMinY = a.transform.position.y - (a.transform.localScale.y / 2);
        float aMaxY = a.transform.position.y + (a.transform.localScale.y / 2);
        float aMinZ = a.transform.position.z - (a.transform.localScale.z / 2);
        float aMaxZ = a.transform.position.z + (a.transform.localScale.z / 2);

        float bMinX = b.transform.position.x - (b.transform.localScale.x / 2);
        float bMaxX = b.transform.position.x + (b.transform.localScale.x / 2);
        float bMinY = b.transform.position.y - (b.transform.localScale.y / 2);
        float bMaxY = b.transform.position.y + (b.transform.localScale.y / 2);
        float bMinZ = b.transform.position.z - (b.transform.localScale.z / 2);
        float bMaxZ = b.transform.position.z + (b.transform.localScale.z / 2);


        if (aMinX<= bMaxX && aMaxX >= bMinX)
        {
            if (aMinY <= bMaxY && aMaxY >= bMinY)
            {
                if (aMinZ <= bMaxZ && aMaxZ >= bMinZ)
                {
                    Debug.Log("hit");
                    return false;
                }
            }
            
        }
        return true;
    }
}
