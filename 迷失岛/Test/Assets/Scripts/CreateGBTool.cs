using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateGBTool
{
    public static Quaternion CalculateQuaternion(Vector3 t1, Vector3 t2)
    {
        Vector3 v = new Vector3(0, 0, 0);
        v.z = (float)(Math.Atan2(t2.y - t1.y, t2.x - t1.x) * 180 / Math.PI);
        //Debug.LogWarningFormat("{0} {1} {2}", t2.y - t1.y, t2.x - t1.x, q.z); ;
        return Quaternion.Euler(v);
    }

    public static float Distance(Vector3 t1, Vector3 t2)
    {
        return (float)Math.Sqrt(Math.Pow(t1.x - t2.x, 2) + Math.Pow(t1.y - t2.y, 2));
    }
}
