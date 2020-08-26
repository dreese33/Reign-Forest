using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    gameObject.GetComponent<Animation>().Play("Female|Run");
                    //anim.Play("Female|Idle");
                    break;
                case CurrentAnimationState.WALK:
                    anim.Play("Walk");
                    break;
                case CurrentAnimationState.RUN:
                    anim.Play("Run");
                    break;
            }
        }
    }
}
