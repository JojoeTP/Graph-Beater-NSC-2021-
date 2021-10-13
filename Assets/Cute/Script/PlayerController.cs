using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    Vector3 move;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //Player movement
    void Move(){
        move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            transform.Translate(move * moveSpeed * Time.deltaTime);
        }
    }
}
