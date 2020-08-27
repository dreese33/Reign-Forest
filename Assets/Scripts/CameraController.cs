using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 lastPos = Vector3.zero;

    private Touch touch;

    public float rotateSpeedMobile = 3.0f;
    public float rotateSpeedComputer = 10.0f;
    public float invertPitch = 1.0f;
    public float deltaOffset = 0.01f;

    private float pitch, yaw = 0.0f;
    public readonly Vector3 offset = new Vector3(0.0f, 17.5f, 3.0f);

    public bool PlayerAlive = true;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraPosition();
        UpdateCameraAngleMobile();
        UpdateCameraAngleComputer();
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
                    UpdateCameraAngleMobile();
                }
            } else if (Input.GetKey(KeyCode.Mouse0))
            {
                    UpdateCameraAngleComputer();
            } else
            {
                lastPos = Vector3.zero;
            }
        }
    }


    void UpdateCameraAngleMobile()
    {
        pitch -= touch.deltaPosition.y * rotateSpeedMobile * invertPitch * Time.deltaTime;
        yaw += touch.deltaPosition.x * rotateSpeedMobile * invertPitch * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -60, 60);
        yaw = Mathf.Clamp(yaw, -60, 60);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }


    void UpdateCameraAngleComputer()
    {
        if (lastPos == Vector3.zero)
        {
            lastPos = Input.mousePosition;
        }

        mousePos = Input.mousePosition - lastPos;

        pitch -= mousePos.y * rotateSpeedComputer * invertPitch * (Time.deltaTime + deltaOffset);
        yaw += mousePos.x * rotateSpeedComputer * invertPitch * (Time.deltaTime + deltaOffset);

        pitch = Mathf.Clamp(pitch, -60, 60);
        yaw = Mathf.Clamp(yaw, -60, 60);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        lastPos += mousePos;
    }


    //This is currently setup for the female character only
    //This code should be executed in the Player class
    void UpdateCameraPosition()
    {
        transform.position = target.position + offset;
    }
}
