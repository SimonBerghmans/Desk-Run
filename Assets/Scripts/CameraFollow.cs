using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private Vector2 cameraRot;
    private float cameraRotx;
    private float cameraSens = 50f;
    public Transform player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        RotateCamera();
        //Follow player staat er omdat camera anders draait met speler
        FollowPlayer();
    }
    private void RotateCamera()
    {
        this.transform.Rotate(0, cameraRotx * cameraSens *Time.deltaTime , 0,0);
    }
    private void FollowPlayer()
    {
        this.transform.position = player.position;
    }
    public void GetCameraRotation(InputAction.CallbackContext context)
    {
        cameraRot = context.ReadValue<Vector2>();
        cameraRotx = cameraRot.x;
    }
    
}

