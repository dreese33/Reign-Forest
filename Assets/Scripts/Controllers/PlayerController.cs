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
    public float speed = 100.0f;
    public bool forwardPressed = false;


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
        if (controller.PlayerAlive == true) 
        {
            Vector3 pos = character.transform.position;

            switch (state)
            {
                case CurrentAnimationState.IDLE:
                    character.GetComponent<Animation>().Play(genderAnimationString + "|Idle");
                    break;
                case CurrentAnimationState.WALK:
                    character.GetComponent<Animation>().Play(genderAnimationString + "|Walk");
                    break;
                case CurrentAnimationState.RUN:
                    character.GetComponent<Animation>().Play(genderAnimationString + "|Run");
                    pos.z += speed * Time.deltaTime;
                    character.transform.position = pos;
                    controller.UpdateCameraPosition();
                    controller.UpdateGunPosition();
                    break;
            }
        }
    }

    
    public void BeginForward()
    {
        forwardPressed = true;
        anim.Stop();
        state = CurrentAnimationState.RUN;
    }


    public void EndForward()
    {
        anim.Stop();
        state = CurrentAnimationState.IDLE;
        forwardPressed = false;
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
