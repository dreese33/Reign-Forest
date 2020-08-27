using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    enum CurrentAnimationState { IDLE, WALK, RUN };

    private Animation anim;
    private CameraController controller;
    private CurrentAnimationState state;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
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
                    gameObject.GetComponent<Animation>().Play("Female|Idle");
                    break;
                case CurrentAnimationState.WALK:
                    gameObject.GetComponent<Animation>().Play("Female|Walk");
                    break;
                case CurrentAnimationState.RUN:
                    gameObject.GetComponent<Animation>().Play("Female|Run");
                    break;
            }
        }
    }
}
