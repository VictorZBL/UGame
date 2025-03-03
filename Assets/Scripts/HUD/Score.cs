using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private int _score = 0;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void ScoreAdd(int enemyScore)
    {
        //Debug.Log("Очко: " + enemyScoreMultiplier);
        _score += enemyScore;
    }

    public void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }
}
