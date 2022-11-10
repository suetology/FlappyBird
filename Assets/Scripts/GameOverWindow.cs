using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    private void Start()
    {
        HideWindow();
        Bird.GetInstance().OnDeath += Bird_OnDeath;
    }

    private void Bird_OnDeath(object sender, EventArgs e)
    {
        ShowWindow();
        scoreText.text = GameManager.GetGameScore().ToString();
        SetHighscore();
        highscoreText.text = GetHighscore().ToString();
    }

    private void SetHighscore()
    {
        if(PlayerPrefs.GetInt("highscore") < GameManager.GetGameScore())
            PlayerPrefs.SetInt("highscore", GameManager.GetGameScore());
    }
    private int GetHighscore()
    {
        return PlayerPrefs.GetInt("highscore");
    }
    private void HideWindow()
    {
        gameObject.SetActive(false);
    }
    private void ShowWindow()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        LoadingScreen.GetInstance().LoadingScreedDarken();
        StartCoroutine(LoadingTime());
    }
    private IEnumerator LoadingTime()
    {
        yield return new WaitForSeconds(LoadingScreen.GetInstance().GetAnimationLength());
        ReloadGame();
    }
    private void ReloadGame()
    {
        SceneManager.LoadScene(0);
        GameManager.StartGame();
    }
}
