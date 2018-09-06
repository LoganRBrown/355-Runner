using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    static private List<AABB> aabbs = new List<AABB>();

    static public void Add(AABB obj)
    {
        aabbs.Add(obj);
    }
    static public void Remove(AABB obj)
    {
        aabbs.Remove(obj);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
		foreach(AABB a in aabbs)
        {
            foreach(AABB b in aabbs)
            {
                if (a == b) continue;
                if (a.isDoneChecking || b.isDoneChecking) continue;

                if (a.CheckOverlap(b))
                {
                    //overlapping!!
                    a.BroadcastMessage("OverlappingAABB", b);
                    b.BroadcastMessage("OverlappingAABB", a);
                }

            }
            a.isDoneChecking = true;
        }
	}
}
