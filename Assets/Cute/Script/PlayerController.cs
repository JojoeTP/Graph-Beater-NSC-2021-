using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    Vector3 diraction;

    public Transform camFollow;


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //Player movement
    void Move(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        diraction = new Vector3(horizontal,0,vertical);

        if(diraction.magnitude >= 0.1f){
            transform.rotation = Quaternion.LookRotation(diraction);
            // transform.Translate(diraction * moveSpeed * Time.deltaTime);
            transform.position += diraction * moveSpeed * Time.deltaTime;
            camFollow.position = transform.position;
        }
    }
}

/*
public float moveSpeed = 4f;
    public Transform cam;
    Vector3 diraction;
    CharacterController characterController;

    public float trunSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start() {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //Player movement
    void Move(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        diraction = new Vector3(horizontal,0,vertical).normalized;

        if(diraction.magnitude >= 0.1f){
            // float targetAugle = Mathf.Atan2(diraction.x,diraction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAugle, ref turnSmoothVelocity, trunSmoothTime);
            // transform.rotation = Quaternion.Euler(0f,angle,0f);

            // Vector3 moveDir = Quaternion.Euler(0f,targetAugle,0f) * Vector3.forward;
            characterController.Move(diraction * moveSpeed * Time.deltaTime);
        }
*/