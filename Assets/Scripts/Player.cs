using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController playerController;
    private float playerSpeed;
    private Vector2 movement;
    private Camera camera;
    private float currentVelocity;
    public Transform cameraCenter;
    private Animator playerAnimator;
    private bool isWalking;
    private bool isRunning;
    private float gravity = -9f;
    private float jumpHeight = 1000f;
    public LayerMask groundLayer;
    public bool isDead;
    private float health;
    private Transform healthBar;
    private Slider healthSlider;


    private void Start()
    {
        SetStartValues();
    }
    private void SetStartValues()
    {
        playerController = GetComponent<CharacterController>();
        camera = FindObjectOfType<Camera>();
        playerAnimator = GetComponentInChildren<Animator>();
        isWalking = false;
        isRunning = false;
        playerSpeed = 1f;
        isDead = false;
        health = 100f;
        healthBar = GameObject.Find("Health Bar").transform;
        healthSlider = healthBar.GetComponentInChildren<Slider>();
        healthSlider.maxValue = health;
        UpdateHealthBar();
    }
    private void FixedUpdate()
    {
        CheckSprint();
        MovePlayer();
        TurnToMovement();
        AnimateMovement();
        ApplyGravity();
        CheckPlayerDead();        
    }
    private void MovePlayer()
    {
        Vector3 movementVector = (camera.transform.forward * movement.y + camera.transform.right * movement.x) *playerSpeed * Time.deltaTime;      
        playerController.Move(movementVector);
    }
   
    //READ INPUT
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && playerController.isGrounded)
        {
            playerController.Move(Vector3.up * jumpHeight * Time.deltaTime);
            playerAnimator.SetBool("isJumping",true);
        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    //

    private void TurnToMovement()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            var targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraCenter.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.05f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    private void AnimateMovement()
    {
        CheckMovement();
        playerAnimator.SetBool("isWalking", isWalking);
        playerAnimator.SetBool("isRunning", isRunning);
        //AnimateJump();
    }
    private void CheckMovement()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    private void CheckSprint()
    {
        if (isRunning)
        {
            playerSpeed = 10f;
        }
        else
        {
            playerSpeed = 1f;
        }
    }

    private void ApplyGravity()
    {
        if (!playerController.isGrounded)
        {
            playerController.Move(Vector3.up * gravity * Time.deltaTime);
        }
    }
    private void AnimateJump()
    {
        RaycastHit hit;
        if (!playerController.isGrounded)
        {
            playerAnimator.SetBool("isFalling", true);
            if (Physics.SphereCast(cameraCenter.position, 0.2f, -Vector3.up, out hit, groundLayer))
            {
                if (playerController.isGrounded)
                {
                    playerAnimator.SetBool("isLanding", true);
                }
            }
        }
        else
        {
            playerAnimator.SetBool("isFalling", false);
            playerAnimator.SetBool("isLanding", false);
            playerAnimator.SetBool("isJumping", false);
            
        }
    }
    private void OnTriggerEnter(Collider collision)
     {
        if(collision.gameObject.tag == "Zombie")
        {
            health -= 30f;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = health;
    }
    
    private void CheckPlayerDead()
    {
        if (health < 0)
        {
            isDead = true;
        }

    }
}
