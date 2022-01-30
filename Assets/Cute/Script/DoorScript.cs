using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorScript : MonoBehaviour
{
    public TMP_Text guideText;
    Animator doorAnimator;
    [Header("Sound")]
    public AudioSource doorSound;

    private void Start() {
        doorAnimator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                doorAnimator.SetBool("isOpen",true);
                doorSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        guideText.gameObject.SetActive(false);

        if(other.gameObject.CompareTag("Player")){
            doorAnimator.SetBool("isOpen",false);
            doorSound.Play();
        }
    }

    void OnTriggerEnter(Collider other){
        guideText.gameObject.SetActive(true);
    }
}
