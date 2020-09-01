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
    public bool gameIsPaused = false;
    public Camera menuCamera;
    public Camera mainCamera;
    public GameObject character;

    private float posZOffset = 15.0f;

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
    }


    void UpdatePauseMenuLocation()
    {
        Vector3 pos = mainCamera.transform.position;
        pos.z += posZOffset;
        pauseMenuUI.transform.position = pos;
    }
}
