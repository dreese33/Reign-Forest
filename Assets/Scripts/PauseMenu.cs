using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Button pauseButton;
    public Button resumeButton;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public bool gameIsPaused = false;
    public Camera menuCamera;
    public Camera mainCamera;
    public GameObject character;

    private float distance = 25.0f;

    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        pauseMenuUI.SetActive(false);
    }

    
    void Pause()
    {
        if (!gameIsPaused)
        {
            gameUI.SetActive(false);

            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;

            UpdatePauseMenuLocation();

            menuCamera.depth += 1;
            mainCamera.GetComponent<AudioListener>().enabled = false;
            menuCamera.GetComponent<AudioListener>().enabled = true;

            menuCamera.transform.position = mainCamera.transform.position;
            menuCamera.transform.rotation = mainCamera.transform.rotation;
        }
    }


    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

        menuCamera.depth -= 1;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        menuCamera.GetComponent<AudioListener>().enabled = false;

        gameUI.SetActive(true);
    }


    void UpdatePauseMenuLocation()
    {
        pauseMenuUI.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y, 0.0f, mainCamera.transform.rotation.w);
        pauseMenuUI.transform.eulerAngles = mainCamera.transform.eulerAngles;
        pauseMenuUI.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
    }



}
