using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public Button fireButton;

    // Start is called before the first frame update
    void Start()
    {
        fireButton.onClick.AddListener(OnFire);
    }


    void OnFire()
    {
        Debug.Log("Fire working");
    }
}
