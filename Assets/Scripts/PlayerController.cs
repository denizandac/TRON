using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    public CharacterController controller;

    public Animator anim;

    public float moveSpeed = 4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float jumpSpeed = 8.0f;
    private float ySpeed;
    float verticalVelocity;

    
    void Start(){
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = Vector3.zero;
        if(Input.GetButtonDown("Jump") && controller.isGrounded){
            ySpeed = jumpSpeed;
        }
    
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            velocity = moveDir.normalized * moveSpeed;
            
        }
        velocity.y = ySpeed;
        controller.Move(velocity * Time.deltaTime);
        
        anim.SetFloat("horizontalSpeed", horizontal);
        anim.SetFloat("verticalSpeed", vertical);

        if(horizontal != 0 || vertical != 0){
            anim.SetBool("isWalking", true);
        }
        else{
            anim.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftControl)){
            moveSpeed = 2f;
            anim.SetBool("isCrouched", true);
        }
        else{
            moveSpeed = 4f;
            anim.SetBool("isCrouched", false);
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = 6f;
            anim.SetBool("isRunning", true);
        }
        else{
            moveSpeed = 4f;
            anim.SetBool("isRunning", false);
        }
    }
}    