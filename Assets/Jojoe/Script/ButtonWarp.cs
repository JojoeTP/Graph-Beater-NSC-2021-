using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonWarp : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject nextLevelWarpPrefab;
    public GameObject currentLevelWarpPrefab;
    public bool correctAnswer = false;
    public TMP_Text guideText;
    public int level = 1;
    public GameObject winCanvas;

    ButtonWarp[] buttonWarp;
    void Start()
    {
        buttonWarp = FindObjectsOfType<ButtonWarp>();
    }
    
    // box collider error
    void OnTriggerEnter(Collider other){
        guideText.gameObject.SetActive(true);
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("E");
                if(correctAnswer == true){
                    correct_MoveToNextLevel();
                    foreach(ButtonWarp n in buttonWarp){
                        n.GetComponent<ButtonWarp>().level += 1;
                    }
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
        if(nextLevelWarpPrefab == null){
            winCanvas.SetActive(true);
            return;
        }
        playerPrefab.transform.position = nextLevelWarpPrefab.transform.position;
    }

    void inCorrect_MoveToCurrentLevel(){
        playerPrefab.transform.position = currentLevelWarpPrefab.transform.position;
    }
}
