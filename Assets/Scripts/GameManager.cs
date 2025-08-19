using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private State state;
    
    private float CountdownToStartTimer = 3f;
    private float GamePlayingTimer;
    private float GamePlayingTimerMax = 120f;

    private int recipesDeliveredCount = 0;

    private bool isGamePaused = false;

    public event EventHandler OnStateGhanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private enum State
    {
        
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Awake()
    {
        instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        DeliveyManager.instance.OnRecipeSuccess += RightPlateDelivered;
        GameInput.instance.OnPauseAction += PlayerPausedTheGame;
        GameInput.instance.OnInteractAction += PlayerCloseTutorial;
    }

    private void PlayerCloseTutorial(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateGhanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void PlayerPausedTheGame(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

   

    private void RightPlateDelivered(object sender, EventArgs e)
    {
        PlayerDeliveredPlate();
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
               
                break;
            
            case State.CountdownToStart:
                CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                    GamePlayingTimer = GamePlayingTimerMax;
                    OnStateGhanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GamePlaying:
               
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnStateGhanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameCountingDown()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownStartTimer()
    {
        return CountdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - ( GamePlayingTimer/GamePlayingTimerMax);
    }

    public int GetRecipesDelivered()
    {
        return recipesDeliveredCount;
    }

    public void PlayerDeliveredPlate()
    {
        recipesDeliveredCount++;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused) 
        {
            OnGameUnpaused?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 1f;
        }
        else
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        }
        
    }
}
