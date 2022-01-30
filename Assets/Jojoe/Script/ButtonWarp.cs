using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonWarp : MonoBehaviour
{
    PlayerController playerPrefab;
    Animator playerAnimator;
    Game3Manager game3Manager;

    public GameObject nextLevelWarpPrefab;
    public GameObject currentLevelWarpPrefab;
    public bool correctAnswer = false;
    public bool lastQuestion = false;
    public TMP_Text guideText;

    void Start()
    {
        playerPrefab = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        game3Manager = FindObjectOfType<Game3Manager>().GetComponent<Game3Manager>();
        playerAnimator = playerPrefab.animator;
    }
    
    // box collider error
    void OnTriggerEnter(Collider other){
        guideText.gameObject.SetActive(true);
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                playerAnimator.SetTrigger("jump");
                if(correctAnswer == true){
                    correct_MoveToNextLevel();
                    game3Manager.level++;
                    game3Manager.SwitchCam();
                }
                else{
                    inCorrect_MoveToCurrentLevel();
                }                
            }
        }
    }

    void OnTriggerExit(Collider other){
        guideText.gameObject.SetActive(false);
    }
    
    void correct_MoveToNextLevel(){
        StartCoroutine("CorrectMove");

        if(lastQuestion){
            game3Manager.winCanvas.SetActive(true);
        }
        // playerPrefab.transform.position = nextLevelWarpPrefab.transform.position;

    }

    void inCorrect_MoveToCurrentLevel(){
        StartCoroutine("InCorrectMove");
        // playerPrefab.transform.position = currentLevelWarpPrefab.transform.position;
    }

    IEnumerator CorrectMove(){
        yield return new WaitForSeconds(2f);

        playerPrefab.transform.position = nextLevelWarpPrefab.transform.position;
        playerAnimator.SetTrigger("test2");
    }

    IEnumerator InCorrectMove(){
        yield return new WaitForSeconds(1f);
        
        playerPrefab.transform.position = currentLevelWarpPrefab.transform.position;
        playerAnimator.SetTrigger("test1");
    }
}
