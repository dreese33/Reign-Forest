using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public Button fireButton;
    private AudioSource audioSource;
    public AudioClip laserNoise1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        fireButton.onClick.AddListener(OnFire);
    }


    void OnFire()
    {
        audioSource.PlayOneShot(laserNoise1);
        GunKickback();
    }


    void GunKickback()
    {
        
    }
}
