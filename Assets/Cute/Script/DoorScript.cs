using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator doorAnimator;

    private void Start() {
        doorAnimator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                doorAnimator.SetBool("isOpen",true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            doorAnimator.SetBool("isOpen",false);
        }
    }
}
