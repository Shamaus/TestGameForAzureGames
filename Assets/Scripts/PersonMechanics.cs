using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMechanics : MonoBehaviour
{
    private int scoreTray;
    private Animator animator;
    private bool wait;

    public Transform trayPersonPoint;
    private float exitTime;

    private GameObject gamePoint;

    void Start()
    {
        exitTime = Random.Range(5, 10);
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        wait = false;
        RandomScore();
        gameObject.GetComponentInParent<StartPoint>().ClosePoint();
    }

    private void RandomScore()
    {
        int red = 0, blue = 0, green = 0;
        red = Random.Range(0, 3);
        if (red == 3)
            return;
        else
        {
            blue = Random.Range(0, 3);
            if (red + blue > 3)
                blue--;
            else
                green = 3 - red - blue;
        }
        scoreTray = blue + red * 10 + green * 100;
    }

    public int GetScoreTray()
    {
        return scoreTray;
    }

    public void TakeTray(GameObject tray)
    {
        if (animator.GetBool("Sad") == false)
        {
            tray.transform.position = trayPersonPoint.position;
            Instantiate(tray, trayPersonPoint, true);
            StartCoroutine(ExitPerson(true));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "StopLine" && animator.GetBool("Sad") == false && animator.GetBool("Carry") == false)
            animator.SetBool("Walk", false);
        if (other.tag == "GamePosition" && wait == false)
        {
            gamePoint = other.gameObject;
            animator.SetBool("Walk", false);
            wait = true;
            StartCoroutine(Lose());
        }
        if (other.name == "DeathLine")
        {
            Destroy(gameObject);
            gameObject.GetComponentInParent<StartPoint>().OpenPoint();
        }
    }

    IEnumerator ExitPerson(bool win)
    {
        if (win)
        {
            animator.SetBool("Carry", true);
            if (gamePoint)
                gamePoint.GetComponent<UiPoint>().PointClose();
        }
        else
            animator.SetBool("Sad", true);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Turn");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Turn");
        yield return new WaitForSeconds(2f);
        animator.SetBool("Walk", true);
    }

    IEnumerator Lose()
    {
        gamePoint.GetComponent<UiPoint>().PersonStart(exitTime, scoreTray);
        yield return new WaitForSeconds(exitTime / 2);
        if (animator.GetBool("Carry") == false)
            animator.SetTrigger("Long Wait");
        yield return new WaitForSeconds(exitTime / 2);
        if (animator.GetBool("Carry") == false)
        {
            StartCoroutine(ExitPerson(false));
            gamePoint.GetComponent<UiPoint>().PointClose();
            gamePoint.GetComponent<UiPoint>().TimeOut();
        }
            
    }
}
