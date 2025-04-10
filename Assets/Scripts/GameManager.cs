using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private State state;
    private float waitingToStartTimer = 1f;
    private float CountdownToStartTimer = 3f;
    private float GamePlayingTimer = 10f;
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

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    state = State.CountdownToStart;
                }
                break;
            
            case State.CountdownToStart:
                CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                }
                break;
            
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
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
}
