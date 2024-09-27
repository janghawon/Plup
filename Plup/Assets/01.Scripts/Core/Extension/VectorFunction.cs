using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorFunction 
{
    public static Vector3 GetRandomVector(Vector3 v1,  Vector3 v2)
    {
        float x = Random.Range(v1.x, v2.x);
        float y = Random.Range(v1.y, v2.y);
        float z = Random.Range(v1.z, v2.z);

        return new Vector3(x, y, z);
    }
}
