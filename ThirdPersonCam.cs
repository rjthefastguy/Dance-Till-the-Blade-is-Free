using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform aimLookAt;

    public CameraStyle currentStyle;

    public GameObject thirdPersonCam;
    
    public GameObject AimCam;

    public bool aiming = false;

    public bool basic = false;

    public enum CameraStyle
    {
        Basic,
        Aim,

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates Orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //Rotate player object
        if(currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if(currentStyle == CameraStyle.Aim)
        {
            Vector3 dirToAimLookAt = aimLookAt.position - new Vector3(transform.position.x, aimLookAt.position.y, transform.position.z);
            orientation.forward = dirToAimLookAt.normalized;

            playerObj.forward = dirToAimLookAt.normalized;
        }
        if(aiming)
        {
            SwitchCameraStyle(CameraStyle.Aim);
        }

        if(basic)
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }

    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {

        thirdPersonCam.SetActive(false);
        AimCam.SetActive(false);

        if(newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if(newStyle == CameraStyle.Aim) AimCam.SetActive(true);

        currentStyle = newStyle;
        basic = false;
        aiming = false;
    }
}
