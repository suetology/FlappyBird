using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreWindow : MonoBehaviour
{
    private static ScoreWindow instance;
    public static ScoreWindow GetInstance() => instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    public void ShowScore()
    {
        gameObject.SetActive(true);
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    public void ChangeScore()
    {
        scoreText.text = GameManager.GetGameScore().ToString();
    }
}
