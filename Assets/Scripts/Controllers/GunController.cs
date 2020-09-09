using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GunsAnimations
{
    IDLE,
    SHOOT
}

public class GunController : MonoBehaviour
{

    public Button fireButton;
    private AudioSource audioSource;
    //private Animation anim;
    private Animator anim;
    private GunsAnimations gunState = GunsAnimations.IDLE;
    public AudioClip laserNoise1;
    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        anim = gun.GetComponent<Animator>();
        anim.enabled = false;
        //anim = gun.GetComponent<Animation>();
        //if (anim == null)
        //{
          //  anim = gun.AddComponent<Animation>();
       // }
        //anim.Stop();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        fireButton.onClick.AddListener(OnFire);
    }


    void Update()
    {/*
        if (anim.play)
        {
            return;
        }

        switch (gunState)
        {
            case GunsAnimations.IDLE:
                anim.sto
                anim.Stop();
                break;
            case GunsAnimations.SHOOT:
                anim.Play("Shoot");
                gunState = GunsAnimations.IDLE;
                break;
        }*/

        if (gunState == GunsAnimations.SHOOT)
        {
            anim.enabled = true;
            gunState = GunsAnimations.IDLE;
            Debug.Log("Time: " + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }


        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && anim.enabled)
        {
            Debug.Log("Done playing");
            anim.enabled = false;
        } else
        {
            Debug.Log("playing");
        }
    }


    void OnFire()
    {
        audioSource.PlayOneShot(laserNoise1);
        gunState = GunsAnimations.SHOOT;
    }
}
