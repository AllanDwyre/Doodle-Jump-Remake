using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool spacialText;

    ScoreController scoreController;
    Vector3 highscorePos;
    int startHighscore;
    private void Start()
    {
        scoreController = FindObjectOfType<ScoreController>().GetComponent<ScoreController>();
        startHighscore = scoreController.HighScore;
        if (highscore != null && spacialText)
            highscorePos = highscore.gameObject.GetComponent<RectTransform>().position;
    }
    private void OnGUI()
    {
        score.text = scoreController.StringScore;

        if (highscore != null && spacialText)
        {
            highscorePos = new Vector3(offset.x, offset.y + (float)( scoreController.HighScore * 0.1f ), offset.z);
            highscore.gameObject.GetComponent<RectTransform>().position = highscorePos;
            highscore.text = scoreController.StringHighScore;
        }
        else if (highscore != null && !spacialText)
        {
            highscore.text = scoreController.StringHighScore;
        }

        if (scoreController.Score > startHighscore)
        {
            StartCoroutine(FadeScoreDisplay(score, 5f));
        }
    }
    private IEnumerator FadeScoreDisplay(TextMeshProUGUI text, float transitionTime)
    {
        float t = 0.0f;
        //Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            text.alpha = ( 1 - ( t / transitionTime ) );
            yield return null;
        }
    }
}
