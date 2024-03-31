using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance
    {
        get; private set;
    }

    public event EventHandler OnStageChanged;
    public event EventHandler OnPauseAction;
    public event EventHandler OnContinueAction;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,

    }

    private State state;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 10f;

    private bool isGamePaused = false;


    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountDownToStart;
            OnStageChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnPauseAction?.Invoke(this, EventArgs.Empty);

        }
        else
        {
            Time.timeScale = 1f;
            OnContinueAction.Invoke(this, EventArgs.Empty);
        }
    }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    gamePlayingTimer = 0f;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartActive()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetCountdownToStartTimer()
    {
        return countDownToStartTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }
}
