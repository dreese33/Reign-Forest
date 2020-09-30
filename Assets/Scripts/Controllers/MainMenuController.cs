using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text text;
    public Slider progressBar;
    public GameObject progressCanvas;
    private bool loading = false;

    void Start()
    {
        progressCanvas.SetActive(false);
        StartBlinking();
    }


    void Update()
    {
        if (Input.touchCount >= 1 || Input.GetMouseButtonDown(0))
        {
            if (!loading)
            {
                StartCoroutine(LoadCharacterSelect());
                loading = true;
            }
        }
    }


    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.b, text.color.g, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.b, text.color.g, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }


    IEnumerator LoadCharacterSelect()
    {
        progressCanvas.SetActive(true);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Single);

        while (!loadScene.isDone)
        {
            if (loadScene.progress + 0.1f < 1.0f)
            {
                progressBar.value = loadScene.progress + 0.1f;
            }
            yield return null;
        }
        
        progressBar.enabled = false;
    }


    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }


    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
