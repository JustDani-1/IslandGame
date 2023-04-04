using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to calculate planes, to make the player walk smoothly
public class PlaneCalc
{
    private Vector3 normal;
    private float d;
    public static float DotProduct(Vector3 v1, Vector3 v2) 
    {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }
    public PlaneCalc(Vector3 n, float _d) //create plane by normal vector
    {
        normal = n;
        d = _d;
    }

    public float SolveForY(float x, float z) 
    {
        // solve ax1 + bx2 + cx3 = d 
        // for x2
        float bx2 = d - (normal.x * x) - (normal.z * z);
        return bx2 / normal.y;
    }
}
