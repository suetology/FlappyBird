using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float STARTING_GAME_SPEED = 15f;
    public const float MEDIUM_GAME_SPEED = 22f;
    public const float IMPOSSIBLE_GAME_SPEED = 30f;

    public static float gameSpeed;
    private static int gameScore;
    public static void SetGameScore()
    {
        gameScore++;
    }

    public static int GetGameScore()
    {
        return gameScore / 2;
    }
    
    private void Start()
    {
        StartGame();
    }
    public static void StartGame()
    {
        gameSpeed = STARTING_GAME_SPEED;
        gameScore = 0;
    }

}
