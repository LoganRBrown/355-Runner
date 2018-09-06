using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    MeshRenderer _mesh; //private field

    public MeshRenderer mesh
    {
        get
        {
            if (!_mesh) _mesh = GetComponent<MeshRenderer>();
            return _mesh;
        }
    }

    public Bounds bounds
    {
        get
        {
            return mesh.bounds;
        }
    }

    [HideInInspector]public bool isDoneChecking = false;

    bool isOverlapping = false;

    void Start()
    {
        CollisionController.Add(this);
    }

    void OnDestroy()
    {
        CollisionController.Remove(this);
    }

    void Update()
    {
        isDoneChecking = false;
        isOverlapping = false;
    }

    void OnDrawGizmos()
    { 
        Gizmos.color = isOverlapping ? Color.red: Color.blue;
        Gizmos.DrawWireCube(transform.position, mesh.bounds.size);
    }
    /// <summary>
    /// Checks to see if some other AABB overlaps this AABB.
    /// </summary>
    /// <param name="othr">The other AABB component to check against.</param>
    /// <returns>if true, the two AABBs overlap</returns>
    public bool CheckOverlap(AABB othr)
    {
        if (othr.bounds.min.x > this.bounds.max.x) return false;
        if (othr.bounds.max.x < this.bounds.min.x) return false;
        if (othr.bounds.min.y > this.bounds.max.y) return false;
        if (othr.bounds.max.y < this.bounds.min.y) return false;
        if (othr.bounds.min.z > this.bounds.max.z) return false;
        if (othr.bounds.max.z < this.bounds.min.z) return false;


        return true;
    }

    void OverlappingAABB(AABB other)
    {
        isOverlapping = true;
    }
}
