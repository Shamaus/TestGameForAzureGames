using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGear : MonoBehaviour
{
    private float rotationZ = 0.0f;

    void FixedUpdate()
    {
        rotationZ--;
        transform.rotation = Quaternion.Euler(new Vector3 (0,0, rotationZ));
        if (rotationZ < -359)
            rotationZ = 0;
    }
}
