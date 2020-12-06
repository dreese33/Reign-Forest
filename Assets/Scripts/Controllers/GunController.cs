using UnityEngine;
using UnityEngine.UI;


public class GunController : MonoBehaviour
{

    [SerializeField]
    Button fireButton;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip laserNoise1;

    [SerializeField]
    ParticleSystem ammo;


    void SetupParticleSystem()
    {
        ammo.Pause();
    }


    void OnFire()
    {
        audioSource.PlayOneShot(laserNoise1);
        ammo.Emit(1);
    }


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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnFire();
        }
    }
}
