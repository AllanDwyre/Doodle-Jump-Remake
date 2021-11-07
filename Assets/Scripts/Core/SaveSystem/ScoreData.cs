using UnityEngine;
[System.Serializable]
public class ScoreData
{
    public int highScore;

    public ScoreData(ScoreController scoreController)
    {
        highScore = scoreController.HighScore;
    }
}
