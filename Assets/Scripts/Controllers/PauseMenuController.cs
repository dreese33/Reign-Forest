using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MenuController
{
    [SerializeField]
    Button resumeButton;

    [SerializeField]
    Button repeatButton;

    [SerializeField]
    Button soundButton;

    [SerializeField]
    Button homeButton;

    public Vector3 screenSize;


    void RestartGame()
    {
        Statics.ResetStatics();
        SceneManager.LoadScene("CharacterSelect");
    }


    void MuteGame()
    {
        //TODO -- Implementation
    }


    void UnmuteGame()
    {
        //TODO -- Implementation
    }


    void UpdateSound()
    {
        if (Statics.GameSoundsEnabled) {
            MuteGame();
            Statics.GameSoundsEnabled = false;
        } else {
            UnmuteGame();
            Statics.GameSoundsEnabled = true;
        }
    }


    void ExitGame()
    {
        //TODO -- Implementation
    }


    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Statics.GameIsPaused = false;

        menuCamera.depth -= 1;
        mainCamera.depth += 1;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        menuCamera.GetComponent<AudioListener>().enabled = false;

        gameUI.SetActive(true);
    }


    void UpdateCameraForMenu()
    {
        RectTransform rt = GetComponent<RectTransform>();
        screenSize = Camera.main.ViewportToWorldPoint(Vector3.up + Vector3.right);

        float sizeX = screenSize.x / rt.rect.width;
        float sizeY = screenSize.y / rt.rect.height;
        rt.localScale = new Vector3(sizeX * 0.25f, sizeY, 1);
    }


    protected override void Start()
    {
        UpdateCameraForMenu();

        resumeButton.onClick.AddListener(Resume);
        repeatButton.onClick.AddListener(RestartGame);
        soundButton.onClick.AddListener(UpdateSound);
        homeButton.onClick.AddListener(ExitGame);
    }
}
