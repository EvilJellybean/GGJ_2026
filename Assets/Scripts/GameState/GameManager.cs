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
    }

    [SerializeField]
    private PlayerMask playerMask;

    public GameState State { get; private set; } = GameState.Playing;

    public PlayerMask PlayerMask => playerMask;

    public int CurrentCollectables { get; private set; }
    public int MaxCollectables {  get; private set; }

    public event System.Action OnPlayerDie;
    public event System.Action OnCollectableCollected;

    private void Awake()
    {
        instance = this;
        MaxCollectables = Object.FindObjectsByType<Collectable>(FindObjectsSortMode.None).Length;
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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CollectCollectable()
    {
        CurrentCollectables++;
        OnCollectableCollected?.Invoke();
    }
}
