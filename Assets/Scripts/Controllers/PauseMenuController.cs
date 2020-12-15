using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MenuController
{
    [SerializeField]
    Button resumeButton;

    public Vector3 screenSize;


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


    protected override void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        screenSize = Camera.main.ViewportToWorldPoint(Vector3.up + Vector3.right);

        float sizeX = screenSize.x / rt.rect.width;
        float sizeY = screenSize.y / rt.rect.height;
        rt.localScale = new Vector3(sizeX * 0.25f, sizeY, 1);

        resumeButton.onClick.AddListener(Resume);
    }
}
