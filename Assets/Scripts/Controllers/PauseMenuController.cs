using System.Collections.Generic;
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
    Button homeButton;

    [SerializeField]
    Button soundButton;

    [SerializeField]
    Button noSoundButton;

    public Vector3 screenSize;


    void RestartGame()
    {
        Statics.ResetStatics();
        SceneManager.LoadScene("CharacterSelect");
    }


    public void MuteGame()
    {
        Statics.GameSoundsEnabled = false;
        AudioListener.pause = true;
        soundButton.gameObject.SetActive(false);
        noSoundButton.gameObject.SetActive(true);
    }


    public void UnmuteGame()
    {
        Debug.Log("Game unmuted");
        Statics.GameSoundsEnabled = true;
        AudioListener.pause = false;
        soundButton.gameObject.SetActive(true);
        noSoundButton.gameObject.SetActive(false);
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

        //soundButton = GameObject.FindGameObjectWithTag("Sound").GetComponent<Button>();
        //noSoundButton = GameObject.FindGameObjectWithTag("No-Sound").GetComponent<Button>();

        resumeButton.onClick.AddListener(Resume);
        repeatButton.onClick.AddListener(RestartGame);
        soundButton.onClick.AddListener(MuteGame);
        noSoundButton.onClick.AddListener(UnmuteGame);
        homeButton.onClick.AddListener(ExitGame);
    }
}
