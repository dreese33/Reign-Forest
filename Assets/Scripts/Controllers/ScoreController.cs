using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    Text scoreUI;

    private int _score = 0;
    public int Score
    {
        get => _score;
        set {
            _score = value;
            UpdateScore();
        }
    }


    private void UpdateScore()
    {
        scoreUI.text = _score.ToString();
    }
}
