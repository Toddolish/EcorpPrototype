using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    Enemy enemyScript;

    [Header("Variables")]
    public float speed;
    public float jumpHeight;
    public float rotationSpeed;
    public float rayDistance;
    public float smooth;
    public float gravity = 1;

    CharacterController controller;
    Vector3 movementShit = Vector3.zero;
    public Animator anim;

    [Header("True or False")]
    public bool hiding = false;
    public bool crouched = false;
    public bool CrouchKeyUp = false;
    public bool sprinting = false;
    public bool readyToBounce = false;

    [Header("PlayerSuperJump")]
    public float SuperTimer;

    void Start()
    {
        anim.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        //enemy
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
        
    }

    void Update()
    {

        Crouch();
        //Jump();
        HidingRanges();
        Sprint();
        Movement();
    }
    void HidingRanges()
    {
        if (hiding == true && crouched == true)
        {
            enemyScript.currentState = Enemy.State.Patrol;
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
            movementShit = new Vector3(0, 0, inputV);
            movementShit = transform.TransformDirection(movementShit);
            movementShit *= speed * Time.deltaTime;

            //rotation
            float horizontalMovement = inputH * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, horizontalMovement);

            if (Input.GetButtonDown("Jump") && !sprinting)
            {
                jumpHeight = 12;
                gravity = 1f;
                movementShit.y = jumpHeight * Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump") && sprinting)
            {
                speed = 6f;
                jumpHeight = 12;
                gravity = 0.7f;
                movementShit.y = jumpHeight * Time.deltaTime;
            }
        }
        movementShit.y -= gravity * Time.deltaTime;
        controller.Move(movementShit);
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && crouched == false)
        {
            anim.SetBool("crouched", true);
           // CrouchKeyUp = true;
            crouched = true;
           // speed = 1.5f;
        }
        else if (Input.GetKeyDown(KeyCode.C) && crouched == true)
        {
            //CrouchKeyUp = false;
            //if (!CrouchKeyUp && !hiding)
            //{
                crouched = false;
                anim.SetBool("crouched", false);
            //    speed = 2f;
            //}
        }
    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !crouched)
        {
            sprinting = true;
            speed = 4f;
        }
        else
        {
            sprinting = false;
            speed = 2f;
        }
    }
    void OnTriggerEnter(Collider PlayerCol)
    {
        if (PlayerCol.gameObject.tag == "Enemy")
        {
            PlayerEaten();
        }
        /*if (PlayerCol.gameObject.tag == "Table")
        {
            hiding = true;
        }*/
        if (PlayerCol.gameObject.tag == "Void")
        {
            PlayerEaten();
        }
    }
    void OnTriggerExit(Collider PlayerCol)
    {
       /* if (PlayerCol.gameObject.tag == "Table")
        {
            hiding = false;
            if(!CrouchKeyUp && !hiding)
            {
                anim.SetBool("crouched", false);
                speed = 2f;
            }
        }*/
    }
    public void PlayerEaten()//Player has been killed by Creature
    {
        SceneManager.LoadScene("Prototype");
    }
   /* void Jump()
    {
        if(readyToBounce)
        {
            SuperTimer += Time.deltaTime;
            jumpHeight = 300;
            gravity = 0.1f;
        }
    }*/
}
