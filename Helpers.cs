using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Vector3 NoY(Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
}
