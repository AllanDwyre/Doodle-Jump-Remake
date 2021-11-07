using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private GameObject HighScorePrefab;
    public int Score { private set; get; } = 0;
    public int HighScore { set; get; } = 0;
    public string StringScore { private set; get; } = "";
    public string StringHighScore { private set; get; } = "";

    private void Start()
    {
        Load();
        if (HighScore > 0)
            Instantiate(HighScorePrefab, new Vector3(0, HighScore * 0.1f, 0), Quaternion.identity);
    }
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (target.y > Score * 0.1f)
        {
            Score = Mathf.CeilToInt(target.y * 10);
            StringScore = ImplementZeroToScore(Score);
        }

        if (Score > HighScore)
        {
            HighScore = Score;
            StringHighScore = ImplementZeroToScore(HighScore);
            Save();
        }
    }

    private void Save()
    {
        SaveSystem.SaveData(this);
    }
    private void Load()
    {
        ScoreData data = SaveSystem.LoadData();
        if (data != null)
        {
            HighScore = data.highScore;
            StringHighScore = ImplementZeroToScore(HighScore);
        }
    }

    private string ImplementZeroToScore(int a)
    {
        string zeroImplement = "";
        for (int i = 0; i < 5 - a.ToString().Length; i++)
        {
            zeroImplement += "0";
        }
        zeroImplement += a.ToString();
        return zeroImplement;
    }
}
//C:/Users/Steven/AppData/LocalLow/DefaultCompany/Doodle Jump Remake