﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{

    public Button maleButton;
    public Button femaleButton;
    public Slider progressBar;
    public GameObject progressCanvas;

    // Start is called before the first frame update
    void Start()
    {
        progressCanvas.SetActive(false);
        maleButton.onClick.AddListener(SelectMale);
        femaleButton.onClick.AddListener(SelectFemale);
    }


    void SelectMale()
    {
        CameraController.gender = CharacterType.MALE;
        StartCoroutine(LoadCharacter());
    }


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


    void SelectFemale()
    {
        CameraController.gender = CharacterType.FEMALE;
        StartCoroutine(LoadCharacter());
    }
}
