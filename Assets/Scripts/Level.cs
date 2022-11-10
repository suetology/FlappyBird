using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_SIZE = 50f;
    private const float PIPE_WIDTH = 10f;
    private const float PIPE_X_POS = 40f;
    private const float PIPE_UPPER_BORDER = 50f;
    private const float PIPE_DESTROY_X = -40f;
    private const float PIPE_SPAWN_MAX_TIME = 2f;
    private const float BIRD_X_POS = 0f;

    private float pipeSpawnTimer = PIPE_SPAWN_MAX_TIME;
    private List<Pipe> pipeList;

    private float currentPipeGapY = 0f;
    private float currentPipeGapSize = 50f;
    private enum Difficulty
    {
         easy,
         medium,
         hard,
         impossible
    }
    private Difficulty ReturnDifficulty()
    {
        if (GameManager.GetGameScore() >= 30)
            return Difficulty.impossible;
        else if (GameManager.GetGameScore() >= 20)
            return Difficulty.hard;
        else if (GameManager.GetGameScore() >= 10)
            return Difficulty.medium;
        else
            return Difficulty.easy;
    }
    private void SetDifficulty(Difficulty dif)
    {
        switch(dif)
        {
            case Difficulty.easy:
                GameManager.gameSpeed = GameManager.STARTING_GAME_SPEED;
                currentPipeGapY = 50f;
                break;
            case Difficulty.medium:
                GameManager.gameSpeed = GameManager.MEDIUM_GAME_SPEED;
                currentPipeGapY = 50f;
                break;
            case Difficulty.hard:
                GameManager.gameSpeed = GameManager.MEDIUM_GAME_SPEED;
                currentPipeGapY = 45f;
                break;
            case Difficulty.impossible:
                GameManager.gameSpeed = GameManager.IMPOSSIBLE_GAME_SPEED;
                currentPipeGapY = 40f;
                break;
        }
    }

    private void Start()
    {
        Bird.GetInstance().OnDeath += Bird_OnDeath;
        pipeList = new List<Pipe>();
        SetDifficulty(ReturnDifficulty());
        currentPipeGapSize = 50f;
        currentPipeGapY = 0f;
        CreatePipes(ChangePipeGapY(), currentPipeGapSize);
        pipeSpawnTimer = CalculateSpawnTime();
    }

    private void Bird_OnDeath(object sender, EventArgs e)
    {
        Bird.GetInstance().birdState = Bird.BirdState.dead;
    }

    private void FixedUpdate()
    {
        if (Bird.GetInstance().birdState == Bird.BirdState.alive)
        {
            MovePipes();
            HandlePipeSpawn();
        }
    }
    private void HandlePipeSpawn()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer <= 0)
        {
            SetDifficulty(ReturnDifficulty());
            pipeSpawnTimer += CalculateSpawnTime();
            CreatePipes(ChangePipeGapY(), currentPipeGapSize);

        }
    }
    private float CalculateSpawnTime()
    {
        return GameManager.STARTING_GAME_SPEED / GameManager.gameSpeed * PIPE_SPAWN_MAX_TIME;
    }
    private float ChangePipeGapY()
    {
        currentPipeGapY = UnityEngine.Random.Range(-(CAMERA_SIZE * 2 - currentPipeGapSize) / 2, (CAMERA_SIZE * 2 - currentPipeGapSize) / 2);
        return currentPipeGapY;
    }
    private void MovePipes()
    {
        for(int i=0; i<pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];


            bool isToTheRightOfBird = pipe.GetPipeXpos() > BIRD_X_POS;
            pipe.MovePipe();
            if(pipe.GetPipeXpos() <= BIRD_X_POS && isToTheRightOfBird)
            {
                Score();
            }
            if (pipe.GetPipeXpos() <= PIPE_DESTROY_X)
            {
                pipe.DestroySelf();
                pipeList.Remove(pipe);
                i--;
            }

        }
    }
    private void Score()
    {
        SoundManager.PlaySound(SoundManager.Sound.score);
        GameManager.SetGameScore();
        ScoreWindow.GetInstance().ChangeScore();
    }
    private void CreatePipes(float gapY, float gapSize)
    {
        CreateLowerPipe(gapY - gapSize * .5f + CAMERA_SIZE);
        CreateUpperPipe(2 * CAMERA_SIZE - gapY - CAMERA_SIZE - gapSize * .5f);
    }
    private void CreateUpperPipe(float height)
    {
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pipeBody);
        pipeBody.position = new Vector3(PIPE_X_POS, PIPE_UPPER_BORDER, pipeBody.position.z);
        SpriteRenderer pbSpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pbSpriteRenderer.size = new Vector2(PIPE_WIDTH, height);
        pipeBody.GetComponent<BoxCollider2D>().size = new Vector2(PIPE_WIDTH, height * 2);
        pipeBody.rotation = Quaternion.Euler(0, 0, 180f);

        Transform pipeHead = Instantiate(GameAssets.GetInstance().pipeHead);
        pipeHead.position = new Vector3(PIPE_X_POS, PIPE_UPPER_BORDER - height, pipeHead.position.z);

        pipeList.Add(new Pipe(pipeHead, pipeBody));
    }
    public void CreateLowerPipe(float height)
    {
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pipeBody);
        pipeBody.position = new Vector3(PIPE_X_POS, -PIPE_UPPER_BORDER, pipeBody.position.z);
        SpriteRenderer pbSpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pbSpriteRenderer.size = new Vector2(PIPE_WIDTH, height);
        pipeBody.GetComponent<BoxCollider2D>().size = new Vector2(PIPE_WIDTH, height * 2);


        Transform pipeHead = Instantiate(GameAssets.GetInstance().pipeHead);
        pipeHead.position = new Vector3(PIPE_X_POS, -PIPE_UPPER_BORDER + height, pipeHead.position.z);

        pipeList.Add(new Pipe(pipeHead, pipeBody));
    }
}
