using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    enum CurrentAnimationState { IDLE, WALK, RUN };

    Animation anim;
    CameraController controller;
    CurrentAnimationState state;
    string genderAnimationString;
    float speed = 100.0f;

    [SerializeField]
    Button forwardButton;

    public GameObject character;
    public bool forwardPressed = false;

    //This needs to become global or static eventually
    bool computerMode = false;

    //Statics
    public static CharacterType gender = CharacterType.FEMALE;


    void Start()
    {
        CharacterSelect();

        anim = character.GetComponent<Animation>();
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        state = CurrentAnimationState.IDLE;
    }

    
    void Update()
    {
        if (CameraController.PlayerAlive) 
        {
            Vector3 pos = character.transform.position;

            switch (state)
            {
                case CurrentAnimationState.IDLE:
                    anim.Play(genderAnimationString + "|Idle");
                    break;
                case CurrentAnimationState.WALK:
                    anim.Play(genderAnimationString + "|Walk");
                    break;
                case CurrentAnimationState.RUN:
                    anim.Play(genderAnimationString + "|Run");
                    pos.z += speed * Time.deltaTime;
                    character.transform.position = pos;
                    break;
            }

            controller.UpdateCameraPosition();
            controller.UpdateTargetPosition();

            if (Input.GetKeyDown(KeyCode.W))
            {
                BeginForwardArrow();
            } else if (Input.GetKeyUp(KeyCode.W))
            {
                EndForwardArrow();
            }
        }
    }


    void BeginForwardArrow()
    {
        computerMode = true;
        if (!forwardPressed)
        {
            forwardButton.interactable = false;
            anim.Stop();
            state = CurrentAnimationState.RUN;
            forwardPressed = true;
        }
    }


    void EndForwardArrow()
    {
        if (forwardPressed)
        {
            forwardButton.interactable = true;
            anim.Stop();
            state = CurrentAnimationState.IDLE;
            forwardPressed = false;
        }
        computerMode = false;
    }

    
    public void BeginForward()
    {
        if (!computerMode)
        {
            forwardPressed = true;
            anim.Stop();
            state = CurrentAnimationState.RUN;
        }
    }


    public void EndForward()
    {
        if (!computerMode)
        {
            forwardPressed = false;
            anim.Stop();
            state = CurrentAnimationState.IDLE;
        }
    }


    void CharacterSelect()
    {
        switch (PlayerController.gender)
        {
            case CharacterType.MALE:
                character = GameObject.Find("MaleLowQuality");
                genderAnimationString = "Male_game_character";
                break;
            case CharacterType.FEMALE:
                character = GameObject.Find("FemaleLowFrames");
                genderAnimationString = "Female";
                break;
        }
    }
}
