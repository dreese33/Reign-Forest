﻿using UnityEngine;
using UnityEngine.EventSystems;


public enum CharacterType {
    MALE,
    FEMALE
}

public class CameraController : MonoBehaviour
{

    Vector3 mousePos;
    Vector3 lastPos = Vector3.zero;
    string[] zombieColliders;

    PauseMenuController pauseMenuController;
    Touch touch;
    int totalTouchCount;

    float rotateSpeedMobile = 3.0f;
    float rotateSpeedComputer = 20.0f;
    float invertPitch = 1.0f;
    float deltaOffset = 0.01f;
    float pitch, yaw = 0.0f;

    int minCameraRotationX = -60;
    int maxCameraRotationX = 60;
    int minCameraRotationY = -10;
    int maxCameraRotationY = 30;


    [SerializeField]
    GameObject maleCharacter;

    [SerializeField]
    GameObject femaleCharacter;

    [SerializeField]
    Transform femaleTarget;

    [SerializeField]
    Transform maleTarget;

    Transform gameTarget;

    Camera mainCamera;

    [SerializeField]
    GameObject gun;

    [SerializeField]
    GameObject ammo;

    [SerializeField]
    GameObject zombie;

    public bool cameraPerspectiveEnabled = true;

    //Constants
    readonly Vector3 OFFSET = new Vector3(0.0f, 0.0f, 3.0f);
    readonly float GUN_OFFSET = 5.0f;


    //Called once in the beginning of the game
    void LoadZombieColliders() {
        Collider[] colliders = zombie.GetComponentsInChildren<Collider>();
        zombieColliders = new string[colliders.Length];
        for (int i = 0; i < colliders.Length; i++) {
            zombieColliders[i] = colliders[i].name;
        }
    }


    bool IsPointerOverUIObject() {
        //Implementation for mobile
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        //Implementation for computer
        //return EventSystem.current.IsPointerOverGameObject();
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
        Vector3 pos = mainCamera.transform.position + GUN_OFFSET * 5.0f * mainCamera.transform.forward;
        pos.x += 1f;
        pos.y += 1f;
        ammo.transform.position = pos;

        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x -= 2.5f;
        eulerAngles.y += 2.5f;
        ammo.transform.eulerAngles = eulerAngles;
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
        transform.position = gameTarget.position + OFFSET;
    }


    void UpdateOnMobile()
    {
        totalTouchCount = Mathf.Clamp(Input.touchCount, 1, 2);
        for (int i = 0; i < totalTouchCount; i++) {
            touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Moved && !IsPointerOverUIObject()) 
            {
                UpdateCameraPosition();
                UpdateCameraAngleMobile();
            }
        }
    }


    void UpdateOnComputer()
    {
        UpdateCameraAngleComputer();
        UpdateCameraPosition();
    }


    public void UpdateGunPosition()
    {
        gun.transform.rotation = new Quaternion(0.0f, mainCamera.transform.rotation.y - 90.0f, 0.0f, mainCamera.transform.rotation.w);
        gun.transform.position = mainCamera.transform.position + GUN_OFFSET * mainCamera.transform.forward;

        Vector3 eulerAngles = mainCamera.transform.eulerAngles;
        eulerAngles.x = 0.0f;
        eulerAngles.y -= 50.0f;
        gun.transform.eulerAngles = eulerAngles;
    }


    void SetupMainCharacter()
    {
        switch (Statics.Gender)
        {
            case CharacterType.FEMALE:
                CenterGameTargetFemale();
                break;
            case CharacterType.MALE:
                CenterGameTargetMale();
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting main controller");
        //Manually set character type here
        mainCamera = Camera.main;
        SetupMainCharacter();

        //Setup score controller
        Statics.ScoreController = FindObjectOfType<ScoreController>();
        Statics.ScoreController.LoadHighScore();

        Statics.DeathController = FindObjectOfType<DeathController>();

        UpdateCameraPosition();
        UpdateGunPosition();

        UpdateCameraAngleMobile();
        UpdateCameraAngleComputer();

        UpdateTargetPosition();

        //Disable gun for now
        gun.SetActive(false);

        //Start game sounds
        pauseMenuController = FindObjectOfType<PauseMenuController>();
        if (Statics.GameSoundsEnabled) {
            pauseMenuController.UnmuteGame();
        } else {
            pauseMenuController.MuteGame();
        }

        //Convert this to a unit test to confirm that the 
        //zombies colliders are actually being loaded properly
        LoadZombieColliders();
    }


    // Update is called once per frame
    void Update()
    {
        if (Statics.PlayerAlive == true)
        {
            if (cameraPerspectiveEnabled) 
            {
                if (Input.touchCount > 0)
                {
                    UpdateOnMobile();
                } else if (Input.GetKey(KeyCode.Mouse0))
                {
                    UpdateOnComputer();
                } else
                {
                    lastPos = Vector3.zero;
                    return;
                }
            }
        }
    }
}
