using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuController
{
    [SerializeField]
    Text score;

    [SerializeField]
    Text highScore;

    float distance = 25.0f;


    void UpdatePauseMenuLocation()
    {
        pauseMenuUI.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y, 0.0f, mainCamera.transform.rotation.w);
        pauseMenuUI.transform.eulerAngles = mainCamera.transform.eulerAngles;
        pauseMenuUI.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
    }


    void UpdateScoreLabel()
    {
        score.text = Statics.ScoreController.Score.ToString();
    }


    void UpdateHighScoreLabel()
    {
        highScore.text = Statics.ScoreController.HighScore.ToString();
    }

    
    void Pause()
    {
        if (!Statics.GameIsPaused)
        {
            gameUI.SetActive(false);

            UpdateScoreLabel();
            UpdateHighScoreLabel();

            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            Statics.GameIsPaused = true;

            UpdatePauseMenuLocation();

            menuCamera.depth += 1;
            mainCamera.depth -= 1;
            mainCamera.GetComponent<AudioListener>().enabled = false;
            menuCamera.GetComponent<AudioListener>().enabled = true;

            menuCamera.transform.position = mainCamera.transform.position;
            menuCamera.transform.rotation = mainCamera.transform.rotation;
        }
    }
    

    protected override void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        pauseMenuUI.SetActive(false);
    }
}
