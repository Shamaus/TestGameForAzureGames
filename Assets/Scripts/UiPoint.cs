using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPoint : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvas;
    public Image bar;

    public GameObject red;
    public GameObject green;
    public GameObject blue;

    private float timeBegin;
    private float timeRate;
    private Transform[] pointUI;


    private void Start()
    {
        pointUI = transform.GetChild(0).gameObject.GetComponent<CanvasPointUI>().SearchPointUI();
    }

    public void PersonStart(float time, int score)
    {
        canvas.SetActive(true);
        timeRate = time;
        timeBegin = Time.time + timeRate;
        CalculateNeedBlocks(score);
        bar.fillAmount = 0;

    }

    private void CalculateNeedBlocks(int score)
    {
        int greenScore, redScore, blueScore;
        greenScore = score / 100;
        score %= 100;
        redScore = score / 10;
        score %= 10;
        blueScore = score / 1;
        int i = ShowNeedBlocks(green, greenScore, 0);
        i = ShowNeedBlocks(red, redScore, i);
        ShowNeedBlocks(blue, blueScore, i);
    }

    private int ShowNeedBlocks(GameObject color, int repeat, int num)
    {
        for (int i = 0; i < repeat; i++)
            Instantiate(color, pointUI[num++]);
        return num;
    }

    public void PointClose()
    {
        Clear();
        canvas.SetActive(false);
    }

    public void TimeOut()
    {
        canvasMain.GetComponent<GameUI>().ReduceScore();
    }

    private void Clear()
    {
        foreach (Transform point in pointUI)
        {
            if (point.childCount > 0)
                Destroy(point.GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        bar.fillAmount = (timeBegin - Time.time) / timeRate;
    }
}
