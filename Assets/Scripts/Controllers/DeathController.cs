using UnityEngine;
using UnityEngine.UI;

public class DeathController : MonoBehaviour {

    [SerializeField]
    Button gunsButton;

    [SerializeField]
    Button fireButton;

    [SerializeField]
    Button target;

    [SerializeField]
    Button forwardButton;

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    Text score;

    GenerateEnemies generator;


    private void SetUIInactive() {
        gunsButton.gameObject.SetActive(false);
        fireButton.gameObject.SetActive(false);
        target.gameObject.SetActive(false);
        forwardButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
    }


    private void SetDeathUIActive() {
        StartCoroutine(generator.FadeIn());
    }


    public void PlayerDied() {
        SetUIInactive();
        SetDeathUIActive();
    }


    void Start()
    {
        generator = FindObjectOfType<GenerateEnemies>();
    }
}