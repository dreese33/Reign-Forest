﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunController : MonoBehaviour
{

    public Button fireButton;
    private AudioSource audioSource;
    //private Animation anim;
    public AudioClip laserNoise1;
    public GameObject gun;
    public ParticleSystem ammo;
    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SetupParticleSystem();
        fireButton.onClick.AddListener(OnFire);
    }


    void OnFire()
    {
        if (first)
        {
            ammo.Emit(1);
            first = false;
            return;
        }
        
        audioSource.PlayOneShot(laserNoise1);
        ammo.Emit(1);
    }

    
    void SetupParticleSystem()
    {
        ammo.Stop();
        OnFire();
    }
}
