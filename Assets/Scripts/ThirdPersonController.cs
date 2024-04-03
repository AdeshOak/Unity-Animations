using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    Animator anim;
    public float acceleration = 10;
    public float deceleration = 10;
    public float maxSpeed = 10;
    float speed = 0;

    public Transform cam;


    //SmoothDampAngle parameters 
    float currentVelocity;
    public float smoothTime = 0.1f;

    //Variables for Jump method
    Rigidbody rb;
    public float jumpVelocity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Getting reference to animator component on our gameobject
        anim = GetComponent<Animator>();

        //Get reference to rigid body component of herc
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get user's input that will determine Herc's forward direction
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

        //Get the magnitude of user's input which will be continous for joystick
        float inputMagnitude = input.magnitude;

        //Normalizing the input so it has magnitude of 1
        Vector3 direction = input/inputMagnitude;

        float currentMaxSpeed = inputMagnitude * maxSpeed;
        if(currentMaxSpeed > maxSpeed){
            currentMaxSpeed = maxSpeed;
        }

        //If user is pressing any keys, then accelerate
        if(inputMagnitude > 0){

                //Compute the input target angle from user
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y ;



                //Compute an "in-between" angle for smoother rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);

                // Turn to face the direction specified by user but smoothed
                transform.rotation = Quaternion.Euler(0,angle,0);

                //If speed is lesser than threshold we increase speed else we reduce speed
                if(speed < currentMaxSpeed){
                    speed+= acceleration * Time.deltaTime;
                }
                else{
                    speed = Mathf.Lerp(speed, currentMaxSpeed, 0.1f);
                }
        }
        //If user isn't pressing keys then decelerate
        else
        {
             speed-= acceleration * Time.deltaTime;
        }
        // Make sure speed doesn't go zero
        if(speed<0){
            speed =0;
        }
        // Set speed to animator
        anim.SetFloat("speed", speed);

        //Call jump method if user presses jump
        if(Input.GetButtonDown("Jump")){
            anim.SetTrigger("jump");
        }

    }
    public void Jump(){
        anim.applyRootMotion = false;
        Vector3 vel = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        rb.velocity = vel;
        
    }
}
