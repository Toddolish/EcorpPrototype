using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    #region variables
    [Header("Variables")]
    float jumpHeight;
    public float rayDistance;
    float smooth;
    float gravity = 1;
    [Space(15)]
    #endregion
    #region Speeds
    [Header("MOVEMENT SPEEDS")]
    [Header("Original Speed")]
    public float originalSpeed;

    [Header("Crouch Speed")]
    public float crouchSpeed;

    [Header("Sprint Speed")]
    public float sprintSpeed;

    [Header("Slide Speed")]
    public float slideSpeed;

    [Header("Walk Speed")]
    public float walkSpeed;

    [Header("Rotation Speed")]
    public float rotationSpeed;

    [Header("Slide Distance")]
    [Range(0.1f,2)]
    public float SlideDistance;

    [Space(50)]

    [Header("JUMP HEIGHTS")]

    [Header("Normal Jump Height")]
    public float normalJumpHeight;

    [Header("Sprint Jump Height")]
    public float sprintJumpHeight;

    [Space(50)]

    [Header("GRAVITY")]

    [Header("Normal Gravity")]
    [Range(0.1f, 2)]
    public float normalGravity;

    [Header("Sprint Gravity")]
    [Range(0.1f, 2)]
    public float sprintJumpGravity;

    [Header("Glide Gravity")]
    [Range(0.1f, 2)]
    public float glideGravity;
    #endregion

    Enemy enemyScript;
    CharacterController controller;
    Vector3 movement = Vector3.zero;
    float slideTimer;

    [Header("Players Animator")]
    Animator anim;

    #region bools
    [Space(50)]

    [Header("True or False")]
    public bool hiding = false;
    public bool crouched = false;
    public bool CrouchKeyUp = false;
    public bool sprinting = false;
    public bool readyToBounce = false;
    public bool sliding = false;
    public bool jumping = false;
#endregion

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        //enemy
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
        
    }

    void Update()
    {
        Crouch();
        Slide();
        HidingRanges();
        Sprint();
        Movement();
    }
    void HidingRanges()
    {
        if (hiding == true && crouched == true)
        {
            //enemyScript.currentState = Enemy.State.feeding; //when player is hiding enemy will go back to feeding state
            enemyScript.seekRadius = 1;
        }
    }
    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }
    public bool IsGrounded()
    {
        RaycastHit hit;
        Ray groundRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundRay, out hit, rayDistance))
        {
            return true; // is grounded
        }
        return false; // not grounded
    }
    void Movement()
    {
        if (IsGrounded())
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            //movement
            movement = new Vector3(0, 0, inputV);
            movement = transform.TransformDirection(movement);
            movement *= originalSpeed * Time.deltaTime;

            //rotation
            float horizontalMovement = inputH * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, horizontalMovement);
            jumping = false;
            Jump();
        }
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement);
    }
    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.C) && sprinting && !jumping)
        {
            sliding = true;
        }
        if (sliding)
        {
            slideTimer += Time.deltaTime;
            anim.SetBool("crouched", true);
            originalSpeed = slideSpeed;
            if (slideTimer > SlideDistance)
            {
                originalSpeed = walkSpeed;
                sliding = false;
                slideTimer = 0;
                anim.SetBool("crouched", false);
            }
        }
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && crouched == false && !sprinting)
        {
            anim.SetBool("crouched", true);
            crouched = true;
            originalSpeed = crouchSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.C) && crouched == true)
        {
            CrouchKeyUp = false;
            if (!CrouchKeyUp && !hiding)
            {
                crouched = false;
                anim.SetBool("crouched", false);
                originalSpeed = walkSpeed;
            }
        }
    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !crouched)
        {
            if (!sliding)
            {
                sprinting = true;
                originalSpeed = sprintSpeed;
            }
            if(sliding)
            {
                originalSpeed = slideSpeed;
            }
        }
        else
        {
            sprinting = false;
            originalSpeed = walkSpeed;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !sprinting && !sliding)
        {
            jumping = true;
            jumpHeight = normalJumpHeight;
            gravity = normalGravity;
            movement.y = jumpHeight * Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && sprinting && !sliding)
        {
            jumping = true;
            jumpHeight = sprintJumpHeight;
            gravity = sprintJumpGravity;
            movement.y = jumpHeight * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider PlayerCol)
    {
        if (PlayerCol.gameObject.tag == "Enemy")
        {
            PlayerEaten();
        }
        if (PlayerCol.gameObject.tag == "HIDING")
        {
            hiding = true;
        }
        if (PlayerCol.gameObject.tag == "Void")
        {
            PlayerEaten();
        }
    }
    void OnTriggerExit(Collider PlayerCol)
    {
        if (PlayerCol.gameObject.tag == "HIDING")
        {
            hiding = false;
            originalSpeed = walkSpeed;
        }
    }
    public void PlayerEaten()//Player has been killed by Creature
    {
        SceneManager.LoadScene("Prototype");
    }
}
