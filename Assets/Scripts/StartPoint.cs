using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private bool status = true;

    public bool GetStatus()
    {
        return status;
    }

    public void ClosePoint()
    {
        status = false;
    }

    public void OpenPoint()
    {
        status = true;
    }
}
