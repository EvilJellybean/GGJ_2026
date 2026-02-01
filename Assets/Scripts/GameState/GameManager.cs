using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Win,
    }

    [SerializeField]
    private PlayerMask playerMask;
    [SerializeField]
    private AudioSource catPickupSfx;
    [SerializeField]
    private AudioSource youDiedSfx;
    [SerializeField]
    private AudioSource youEscapedSfx;

    private Dictionary<int, float> dangerSources = new Dictionary<int, float>();

    public GameState State { get; private set; } = GameState.Playing;

    public PlayerMask PlayerMask => playerMask;

    public int CurrentCollectables { get; private set; }
    public int MaxCollectables {  get; private set; }

    public bool AllCollectablesFound => CurrentCollectables >= MaxCollectables;

    public float DangerAmount
    {
        get
        {
            float maxDanger = 0;

            Dictionary<int, float>.ValueCollection dangerList = dangerSources.Values;
            foreach(float danger in dangerList)
            {
                maxDanger = Mathf.Max(maxDanger, danger);
            }
            return maxDanger;
        }
    }

    public event System.Action OnPlayerDie;
    public event System.Action OnPlayerWin;
    public event System.Action OnCollectableCollected;

    private void Awake()
    {
        instance = this;
        MaxCollectables = Object.FindObjectsByType<Collectable>(FindObjectsSortMode.None).Length;
    }

    public void KillPlayer()
    {
        if(State != GameState.Playing)
        {
            return;
        }
        if (youDiedSfx != null)
        {
            youDiedSfx.Play();
        }
        State = GameState.Dead;
        OnPlayerDie?.Invoke();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CollectCollectable()
    {
        CurrentCollectables++;
        OnCollectableCollected?.Invoke();

        if(catPickupSfx != null)
        {
            catPickupSfx.Play();
        }
    }

    public void WinGame()
    {
        if (State != GameState.Playing)
        {
            return;
        }
        if(youEscapedSfx != null)
        {
            youEscapedSfx.Play();
        }
        State = GameState.Win;
        OnPlayerWin?.Invoke();
    }

    public void UpdateDangerSource(GameObject source, float dangerAmount)
    {
        int id = source.GetInstanceID();
        if (dangerAmount > 0)
        {
            if (!dangerSources.ContainsKey(id))
            {
                dangerSources.Add(id, dangerAmount);
            }
            else
            {
                dangerSources[id] = dangerAmount;
            }
        }
        else
        {
            if (dangerSources.ContainsKey(id))
            {
                dangerSources.Remove(id);
            }
        }
    }
}
