using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public AudioClip AudioClip;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    [SerializeField] float restartDelay = 1f;
    AudioManager audioManager;
    public bool gameHasEnded = false;
    private void Awake()
    {
        if (_instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        audioManager = AudioManager.Instance;
        audioManager.PlayMusic(AudioClip);
        audioManager.MusicFade = 4f;
    }
    private void Update()
    {
        if (Instance != this)
            Destroy(gameObject);

        if (audioManager.MusicCanFade)
        {
            audioManager.PlayMusicWithCrossFade(audioManager.RandomMusic(), audioManager.MusicFade);
        }
    }
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            Debug.Log("Is really dead");
            gameHasEnded = true;
            FindObjectOfType<CameraBehavior>().GetComponent<CameraBehavior>().cameraBehaviors = CameraBehaviors.EndGameFollow;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Core");
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        SceneManager.LoadScene("Environnement", LoadSceneMode.Additive);
        SceneManager.LoadScene("BackGround", LoadSceneMode.Additive);
    }

}
