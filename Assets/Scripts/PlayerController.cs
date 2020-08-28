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

    public Button forwardButton;
    public GameObject character;
    public float playerVelocity = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        anim = character.GetComponent<Animation>();
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        state = CurrentAnimationState.IDLE;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (controller.PlayerAlive == true) 
        {
            if (anim.isPlaying) {
                return;
            }

            switch (state)
            {
                case CurrentAnimationState.IDLE:
                    character.GetComponent<Animation>().Play("Female|Idle");
                    break;
                case CurrentAnimationState.WALK:
                    character.GetComponent<Animation>().Play("Female|Walk");
                    break;
                case CurrentAnimationState.RUN:
                    character.GetComponent<Animation>().Play("Female|Run");
                    break;
            }
        }
    }

    
    public void BeginForward()
    {
        anim.Stop();
        state = CurrentAnimationState.RUN;
    }


    public void EndForward()
    {
        anim.Stop();
        state = CurrentAnimationState.IDLE;
    }
}
