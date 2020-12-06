using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    CameraController controller;


    public void BeginDraging()
    {
        controller.cameraPerspectiveEnabled = false;
    }


    public void EndDraging()
    {
        controller.cameraPerspectiveEnabled = true;
    }


    void Start()
    {
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
}
