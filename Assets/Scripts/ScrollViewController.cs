using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    private CameraController controller;

    void Start()
    {
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public void BeginDraging()
    {
        Debug.Log("Drag began");
        controller.cameraPerspectiveEnabled = false;
    }


    public void EndDraging()
    {
        Debug.Log("Drag Ended");
        controller.cameraPerspectiveEnabled = true;
    }
}
