using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bird : MonoBehaviour
{
    private static Bird instance;
    public static Bird GetInstance() => instance;
    private void Awake()
    {
        instance = this;
    }

    private const float CAMERA_SIZE = 50f;

    private Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    public BirdState birdState;

    public event EventHandler OnDeath;
    public enum BirdState
    {
        waitingToStart,
        alive,
        dead
    }

    private void Start()
    {
        birdState = BirdState.waitingToStart;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }
    private void BirdJump()
    {
        if (birdState == BirdState.alive)
        {
            SoundManager.PlaySound(SoundManager.Sound.wing);
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (birdState == BirdState.waitingToStart)
            StartGame();
    }
    private void StartGame()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        birdState = BirdState.alive;
        GameStartWindow.GetInstance().CloseStartWindow();
        ScoreWindow.GetInstance().ShowScore();
        BirdJump();
    }
    void Update()
    {
        CheckBirdHeight();

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            BirdJump();
        }
    }

    private void CheckBirdHeight()
    {
        if(transform.position.y + transform.localScale.y/2 >= CAMERA_SIZE || 
           transform.position.y - transform.localScale.y/2 <= -CAMERA_SIZE)
        {
            if (birdState == BirdState.alive)
                Death(SoundManager.Sound.die);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(birdState == BirdState.alive)
            Death(SoundManager.Sound.hit); 
    }
    private void Death(SoundManager.Sound deathSound)
    {
        birdState = BirdState.dead;
        rb.bodyType = RigidbodyType2D.Static;
        if (OnDeath != null)
        {
            OnDeath(this, EventArgs.Empty);
        }
        SoundManager.PlaySound(deathSound);
    }
}
