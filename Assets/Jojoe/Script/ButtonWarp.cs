using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonWarp : MonoBehaviour
{
    PlayerController playerPrefab;
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
                    game3Manager.level++;
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
        if(lastQuestion){
            game3Manager.winCanvas.SetActive(true);
        }
        playerPrefab.transform.position = nextLevelWarpPrefab.transform.position;
    }

    void inCorrect_MoveToCurrentLevel(){
        playerPrefab.transform.position = currentLevelWarpPrefab.transform.position;
    }
}
