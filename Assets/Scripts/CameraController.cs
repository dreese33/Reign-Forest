using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Touch touch;

    public float rotateSpeed = 3.0f;
    public int invertPitch = 1;

    private float pitch, yaw = 0.0f;
    public readonly Vector3 offset = new Vector3(0.0f, 17.5f, 3.0f);

    public bool PlayerAlive = true;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraPosition();
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerAlive == true)
        {
            UpdateCameraPosition();
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved) 
                {
                    UpdateCameraAngle();
                }
            }
        }
    }

    void UpdateCameraAngle()
    {
        pitch -= touch.deltaPosition.y * rotateSpeed * invertPitch * Time.deltaTime;
        yaw += touch.deltaPosition.x * rotateSpeed * invertPitch * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -60, 60);
        yaw = Mathf.Clamp(yaw, -60, 60);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }


    //This is currently setup for the female character only
    //This code should be executed in the Player class
    void UpdateCameraPosition()
    {
        transform.position = target.position + offset;
    }

}
