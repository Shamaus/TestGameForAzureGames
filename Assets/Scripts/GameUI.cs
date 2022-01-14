using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Text score;
    public Text scoreFinal;
    public Text time;

    private Transform[] stagesGames;

    private float gameTime = 60f;
    private int scoreNum;

    private void Start()
    {
        SaveStagesGame();
        stagesGames[0].gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void SaveStagesGame()
    {
        stagesGames = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
            stagesGames[i++] = t;
        foreach (Transform stage in stagesGames)
            stage.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        stagesGames[0].gameObject.SetActive(false);
        stagesGames[1].gameObject.SetActive(true);
        gameTime += Time.time;
        Time.timeScale = 1f;
    }

    public void IncreaseScore()
    {
        scoreNum++;
    }

    public void ReduceScore()
    {
        scoreNum--;
    }

    private void Update()
    {
        if (stagesGames[1].gameObject.activeSelf)
        {
            score.text = $"Score:{scoreNum}";
            if (gameTime - Time.time > 0)
                time.text = $"Time:{Mathf.Round(gameTime - Time.time)}";
            else
                GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        stagesGames[1].gameObject.SetActive(false);
        stagesGames[2].gameObject.SetActive(true);
        scoreFinal.text = $"Score:{scoreNum}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
