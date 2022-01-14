using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMechanics : MonoBehaviour
{
    private GameObject[] trays;
    private int numberTray;
    private bool readyToTray;
    private int numPosBlock = 0;

    private float nextTime = 0.0f;
    private float timeRate = 1.3f;

    public Transform startConveyor;
    public Transform pointStayTray;

    void Start()
    {
        trays = GameObject.FindGameObjectsWithTag("Tray");
        numberTray = 0;
        readyToTray = false;
        StartTray();
    }

    private void StartTray()
    {
        trays[numberTray].transform.position = pointStayTray.position;
        readyToTray = true;
    }

    public void NextTray()
    {
        if (readyToTray)
        {
            numPosBlock = 0;
            trays[numberTray].transform.position = startConveyor.position;
            trays[numberTray].GetComponent<TrayMechanics>().StartWork();
            trays[numberTray].GetComponent<TrayMechanics>().CalculateScore();
            numberTray++;
            if (numberTray == trays.Length)
                numberTray = 0;
            readyToTray = false;
        }
        StartCoroutine(WaitTray());
    }

    IEnumerator WaitTray()
    {
        yield return new WaitForSeconds(timeRate);
        if (Time.time >= nextTime)
        {
            nextTime = Time.time + timeRate;
            StartTray();
        }
    }

    public void TrayAddRed()
    {
        TrayAddBlock("Red");
    }

    public void TrayAddGreen()
    {
        TrayAddBlock("Green");
    }
    public void TrayAddBlue()
    {
        TrayAddBlock("Blue");
    }

    private void TrayAddBlock(string color)
    {
        if (readyToTray)
            trays[numberTray].GetComponent<TrayMechanics>().AddBlock(numPosBlock++, color);
        if (numPosBlock > 2)
            numPosBlock = 0;
    }
}

