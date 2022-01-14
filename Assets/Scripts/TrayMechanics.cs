using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayMechanics : MonoBehaviour
{
    private bool work;

    private Vector3 startPosition;
    private Transform[] pointBlocks;

    public GameObject redBlock;
    public GameObject greenBlock;
    public GameObject blueBlock;

    public GameObject canvas;

    private int sumTrayBlocks = 0;

    private void Start()
    {
        startPosition = transform.position;
        SearchPointBlocks();
    }

    private void SearchPointBlocks()
    {
        pointBlocks = new Transform[transform.childCount - 1];
        int i = 0;
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "Point")
                pointBlocks[i++] = t;
        }
    }

    private void Update()
    {
        Move();
    }

    public void StartWork()
    {
        work = true;
    }

    public int GetSumTrayBlocks()
    {
        return sumTrayBlocks;
    }

    private void Move()
    {
        if (work)
            transform.Translate(new Vector3 (2f,0,0) * Time.deltaTime);
    }

    public void DestroyTray()
    {
        work = false;
        transform.position = startPosition;
        ClearTray();
        sumTrayBlocks = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Basket")
        {
            DestroyTray();
            canvas.GetComponent<GameUI>().ReduceScore();
        } 
        else if (other.transform.parent.gameObject.tag == "Person")
        {
            if (other.transform.parent.gameObject.GetComponent<PersonMechanics>().GetScoreTray() == sumTrayBlocks)
            { 
                other.transform.parent.gameObject.GetComponent<PersonMechanics>().TakeTray(gameObject);
                canvas.GetComponent<GameUI>().IncreaseScore();
                DestroyTray();
            }
        }
    }

    public void AddBlock(int numPos, string color)
    {
        ClearTray(numPos);
        switch (color)
        {
            case "Red":
                Instantiate(redBlock, pointBlocks[numPos]);
                break;
            case "Blue":
                Instantiate(blueBlock, pointBlocks[numPos]);
                break;
            case "Green":
                Instantiate(greenBlock, pointBlocks[numPos]);
                break;
            default:
                break;
        }
    }
    
    private void ClearTray()
    {
        foreach(Transform point in pointBlocks)
            if (point.childCount > 0)
                Destroy(point.GetChild(0).gameObject);
    }

    private void ClearTray(int numPos)
    {
        if (pointBlocks[numPos].childCount > 0)
            Destroy(pointBlocks[numPos].GetChild(0).gameObject);
    }

    public void CalculateScore()
    {
        foreach (Transform point in pointBlocks)
        {
            if (point.childCount > 0)
            {
                if (point.GetChild(0).gameObject.tag == "BlueBlock")
                    sumTrayBlocks += 1;
                if (point.GetChild(0).gameObject.tag == "RedBlock")
                    sumTrayBlocks += 10;
                if (point.GetChild(0).gameObject.tag == "GreenBlock")
                    sumTrayBlocks += 100;
            }
        }
    }
}
