using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum CharacterType {
    MALE,
    FEMALE
}

public class CameraController : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 lastPos = Vector3.zero;

    private Touch touch;
    private int totalTouchCount;

    public float rotateSpeedMobile = 3.0f;
    public float rotateSpeedComputer = 20.0f;
    public float invertPitch = 1.0f;
    public float deltaOffset = 0.01f;

    private float pitch, yaw = 0.0f;

    public readonly Vector3 offset = new Vector3(0.0f, 0.0f, 3.0f);

    public bool PlayerAlive = true;
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public Transform femaleTarget;
    public Transform maleTarget;
    public Transform gameTarget;

    //STATIC variable beware
    public static CharacterType gender = CharacterType.FEMALE;

    public bool cameraPerspectiveEnabled = true;

    public Camera mainCamera;
    public GameObject gun;
    private readonly float gunOffset = 5.0f;

    private int minCameraRotationX = -60;
    private int maxCameraRotationX = 60;
    private int minCameraRotationY = -10;
    private int maxCameraRotationY = 30;


    public Button targetButton;
    public GameObject ammo;
    private float initialTargetButtonYAnchor;


    // Start is called before the first frame update
    void Start()
    {
        //Manually set character type here
        mainCamera = Camera.main;
        initialTargetButtonYAnchor = 50f;
        SetupMainCharacter();

        UpdateCameraPosition();
        UpdateGunPosition();

        UpdateCameraAngleMobile();
        UpdateCameraAngleComputer();

        UpdateTargetPosition();

        //Disable gun for now
        gun.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerAlive == true)
        {
            if (cameraPerspectiveEnabled) 
            {
                if (Input.touchCount > 0)
                {
                    totalTouchCount = Mathf.Clamp(Input.touchCount, 1, 2);
                    for (int i = 0; i < totalTouchCount; i++) {
                        touch = Input.GetTouch(i);
                        if (touch.phase == TouchPhase.Moved && !IsPointerOverUIObject()) 
                        {
                            //Perform mobile updates here
                            UpdateCameraPosition();
                            //UpdateGunPosition();
                            
                            UpdateCameraAngleMobile();
                        }
                    }
                } else if (Input.GetKey(KeyCode.Mouse0))
                {
                        //Perform computer updates here
                        UpdateCameraAngleComputer();

                        UpdateCameraPosition();
                        //UpdateGunPosition();
                } else
                {
                    //UpdateGunPosition();
                    lastPos = Vector3.zero;
                    return;
                }
            }
        }
    }


    private bool IsPointerOverUIObject() {
        //Implementation for mobile
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        //Implementation for computer
        //return EventSystem.current.IsPointerOverGameObject();
    }


    void UpdateCameraAngleMobile()
    {
        pitch -= touch.deltaPosition.y * rotateSpeedMobile * invertPitch * Time.deltaTime;
        yaw += touch.deltaPosition.x * rotateSpeedMobile * invertPitch * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minCameraRotationY, maxCameraRotationY);
        yaw = Mathf.Clamp(yaw, minCameraRotationX, maxCameraRotationX);

        UpdateTargetPosition();

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

        pitch = Mathf.Clamp(pitch, minCameraRotationY, maxCameraRotationY);
        yaw = Mathf.Clamp(yaw, minCameraRotationX, maxCameraRotationX);

        UpdateTargetPosition();

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        lastPos += mousePos;
    }


    public void UpdateCameraPosition()
    {
        transform.position = gameTarget.position + offset;
    }


    public void UpdateGunPosition()
    {
        gun.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y - 90.0f, 0.0f, mainCamera.transform.rotation.w);
        gun.transform.position = mainCamera.transform.position + gunOffset * mainCamera.transform.forward;

        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x = 0.0f;
        eulerAngles.y -= 50.0f;
        gun.transform.eulerAngles = eulerAngles;
    }


    void SetupMainCharacter()
    {
        switch (gender)
        {
            case CharacterType.FEMALE:
                CenterGameTargetFemale();
                break;
            case CharacterType.MALE:
                CenterGameTargetMale();
                break;
        }
    }


    void CenterGameTargetMale()
    {
        gameTarget = maleTarget;
        femaleCharacter.SetActive(false);
        maleCharacter.SetActive(true);

        Vector3 pos = maleCharacter.transform.position;
        pos.x = 50;
        maleCharacter.transform.position = pos;
    }


    void CenterGameTargetFemale()
    {
        gameTarget = femaleTarget;
        maleCharacter.SetActive(false);
        femaleCharacter.SetActive(true);

        Vector3 pos = femaleCharacter.transform.position;
        pos.x = 50;
        femaleCharacter.transform.position = pos;
    }


    public void UpdateTargetPosition()
    {
        Vector2 anchorPos = targetButton.GetComponent<RectTransform>().anchoredPosition;
        anchorPos.y = pitch * 1.2f + initialTargetButtonYAnchor;
        targetButton.GetComponent<RectTransform>().anchoredPosition = anchorPos;

        /*  Update the ammo's position to align with the target and the main camera  */
        ammo.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y, 0.0f, mainCamera.transform.rotation.w);
        ammo.transform.position = mainCamera.transform.position + gunOffset * mainCamera.transform.forward;

        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x = 0.0f;
        eulerAngles.y -= 50.0f;
        ammo.transform.eulerAngles = eulerAngles;
    }
}
