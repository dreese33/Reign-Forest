using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    Button pauseButton;

    [SerializeField]
    Button resumeButton;

    [SerializeField]
    GameObject pauseMenuUI;

    [SerializeField]
    GameObject gameUI;

    public bool gameIsPaused = false;
    public Camera menuCamera;
    public Camera mainCamera;

    float distance = 25.0f;


    void UpdatePauseMenuLocation()
    {
        pauseMenuUI.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y, 0.0f, mainCamera.transform.rotation.w);
        pauseMenuUI.transform.eulerAngles = mainCamera.transform.eulerAngles;
        pauseMenuUI.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
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
            mainCamera.depth -= 1;
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
        mainCamera.depth += 1;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        menuCamera.GetComponent<AudioListener>().enabled = false;

        gameUI.SetActive(true);
    }


    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        pauseMenuUI.SetActive(false);
    }
}
