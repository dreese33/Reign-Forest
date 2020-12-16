using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField]
    Button maleButton;

    [SerializeField]
    Button femaleButton;

    [SerializeField]
    Slider progressBar;

    [SerializeField]
    GameObject progressCanvas;

    bool loading = false;


    IEnumerator LoadCharacter()
    {
        progressCanvas.SetActive(true);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        Debug.Log("Pro :" + loadScene.progress);

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


    void SelectMale()
    {
        if (!loading)
        {
            Statics.Gender = CharacterType.MALE;
            StartCoroutine(LoadCharacter());
            loading = true;
        }
    }


    void SelectFemale()
    {
        if (!loading)
        {
            Statics.Gender = CharacterType.FEMALE;
            StartCoroutine(LoadCharacter());
            loading = true;
        }
    }


    void Start()
    {
        progressCanvas.SetActive(false);
        maleButton.onClick.AddListener(SelectMale);
        femaleButton.onClick.AddListener(SelectFemale);
    }
}
