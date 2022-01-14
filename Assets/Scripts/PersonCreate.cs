using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCreate : MonoBehaviour
{
    private Transform[] startPoints;
    public GameObject person;

    public float timeRate = 5f;

    private void Start()
    {
        SearchPointBlocks();
        StartCoroutine(Create());
    }

    private void SearchPointBlocks()
    {
        startPoints = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "StartGamePosition")
                startPoints[i++] = t;
        }
    }

    IEnumerator Create()
    {
        yield return new WaitForSeconds(timeRate);
        foreach (Transform point in startPoints)
            if (point.gameObject.GetComponent<StartPoint>().GetStatus() == true)
            {
                Instantiate(person, point);
                break;
            }
        StartCoroutine(Create());
    }
}
