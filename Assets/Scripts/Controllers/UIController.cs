using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{

    public GameObject gunsObjectView;
    public bool gunsObjectViewShowing;
    private ScrollRect gunsScrollRect;

    public Button gunsButton;
    public Sprite gunsButtonSprite;
    public Sprite gunsButtonSpriteFlip;

    void Start()
    {

        //Set invisible scroll bar at beginning of game
        gunsObjectViewShowing = false;
        gunsObjectView.gameObject.SetActive(false);
        gunsScrollRect = gunsObjectView.GetComponent<ScrollRect>();

        //Setup gunsbutton action listener
        gunsButton.onClick.AddListener(UpdateGunsView);
    }


    void UpdateGunsView()
    {
        if (gunsObjectViewShowing) 
        {
            DisableGunsView();
        } else {
            EnableGunsView();
        }
    }


    void EnableGunsView()
    {
        Vector3 pos = gunsButton.transform.position;
        pos.x += 500;
        gunsButton.transform.position = pos;

        gunsButton.image.overrideSprite = gunsButtonSprite;

        gunsObjectViewShowing = true;
        gunsObjectView.gameObject.SetActive(true);
    }


    void DisableGunsView()
    {
        Vector3 pos = gunsButton.transform.position;
        pos.x -= 500;
        gunsButton.transform.position = pos;

        gunsButton.image.overrideSprite = gunsButtonSpriteFlip;

        gunsObjectViewShowing = false;
        gunsObjectView.gameObject.SetActive(false);
    }
}
