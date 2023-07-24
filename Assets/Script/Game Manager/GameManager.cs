using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text scoreText;
    public Text highScoreText;
    private int score;
    private int highScore;

    public float scoreIncrementInterval = 2.0f; // Time interval for automatic score increment
    public int scoreIncrementAmount = 1; // Amount to increment the score

    private float timer = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        score = 0;
        LoadHighScore();
        UpdateScoreText();
    }

    private void Update()
    {
        HandleAutomaticScoreIncrement();
    }

    private void HandleAutomaticScoreIncrement()
    {
        timer += Time.deltaTime;
        if (timer >= scoreIncrementInterval)
        {
            IncrementScore(scoreIncrementAmount);
            timer = 0f;
        }
    }

    public void IncrementScore(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
