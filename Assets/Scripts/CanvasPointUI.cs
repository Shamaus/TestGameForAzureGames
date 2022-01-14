using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPointUI : MonoBehaviour
{
    public Transform[] SearchPointUI()
    {
        Transform[] pointUI = new Transform[3];
        int i = 0;
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "PointUI")
                pointUI[i++] = t;
        }
        return pointUI;
    }
}
