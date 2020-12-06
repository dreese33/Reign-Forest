using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    ScrollRect gunsScrollRect;
    public bool gunsObjectViewShowing;

    [SerializeField]
    GameObject gunsObjectView;

    [SerializeField]
    Button gunsButton;

    [SerializeField]
    Sprite gunsButtonSprite;

    [SerializeField]
    Sprite gunsButtonSpriteFlip;


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


    void UpdateGunsView()
    {
        if (gunsObjectViewShowing) 
        {
            DisableGunsView();
        } else {
            EnableGunsView();
        }
    }


    void Start()
    {
        //Set invisible scroll bar at beginning of game
        gunsObjectViewShowing = false;
        gunsObjectView.gameObject.SetActive(false);
        gunsScrollRect = gunsObjectView.GetComponent<ScrollRect>();

        //Setup gunsbutton action listener
        gunsButton.onClick.AddListener(UpdateGunsView);
    }
}
