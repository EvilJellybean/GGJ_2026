using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public enum GameState
    {
        Playing,
        Dead,
    }

    public GameState State { get; private set; } = GameState.Playing;

    public event Action OnPlayerDie;

    private void Awake()
    {
        instance = this;
    }

    public void KillPlayer()
    {
        if(State == GameState.Dead)
        {
            return;
        }
        State = GameState.Dead;
        OnPlayerDie?.Invoke();
    }
}
