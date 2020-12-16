using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    Text scoreUI;

    [SerializeField]
    Text scorePause;

    [SerializeField]
    Text highScorePause;

    int _score = 0;
    public int Score
    {
        get => _score;
        set {
            _score = value;
            UpdateScoreLabel();
        }
    }

    int _highScore = 0;
    public int HighScore {
        get => _highScore;
        set {
            if (value > _highScore) {
                _highScore = value;
                UpdateHighScore();
            }
        }
    }


    void UpdateScoreLabel()
    {
        scoreUI.text = _score.ToString();
        //if (scorePause != null)
            //scorePause.text = _score.ToString();
        HighScore = _score;
    }


    void UpdateHighScore()
    {
        PlayerPrefs.SetInt("highscore", _highScore);
        PlayerPrefs.Save();
        //if (highScorePause != null)
            //highScorePause.text = _highScore.ToString();
    }


    public void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt("highscore", 0);
    }
}
