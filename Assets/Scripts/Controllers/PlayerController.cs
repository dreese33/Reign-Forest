using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    enum CurrentAnimationState { IDLE, WALK, RUN };

    private Animation anim;
    private CameraController controller;
    private CurrentAnimationState state;
    private string genderAnimationString;

    public Button forwardButton;
    public GameObject character;
    private float speed = 100.0f;
    public bool forwardPressed = false;
    private bool computerMode = false;


    // Start is called before the first frame update
    void Start()
    {
        CharacterSelect();

        anim = character.GetComponent<Animation>();
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        state = CurrentAnimationState.IDLE;
    }

    // Update is called once per frame
    
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
                    //controller.UpdateGunPosition();
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


    private void CharacterSelect()
    {
        switch (CameraController.gender)
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
