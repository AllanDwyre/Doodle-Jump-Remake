using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject inGameUI;


    GameObject currentUI;
    DepthOfField depthOfField;
    bool isPaused = false;

    private void Start()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);

        currentUI = inGameUI;
        if (volume.profile.TryGet(out depthOfField))
        {
            depthOfField.active = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            ChangeUI(pauseUI);
            Time.timeScale = 0.0f;
            isPaused = !isPaused;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ChangeUI(inGameUI);
            Time.timeScale = 1.0f;
            isPaused = !isPaused;
        }


        if (GameManager.Instance.gameHasEnded)
        {
            ChangeUI(gameOverUI);
            Time.timeScale = 0.5f;
            GameManager.Instance.gameHasEnded = false;
        }
    }
    public void Resume()
    {
        ChangeUI(inGameUI);
        Time.timeScale = 1.0f;
        isPaused = !isPaused;
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.Restart();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ChangeUI(GameObject newUI)
    {
        currentUI.SetActive(false);
        newUI.SetActive(true);
        currentUI = newUI;
        depthOfField.active = !depthOfField.active;
    }
}
