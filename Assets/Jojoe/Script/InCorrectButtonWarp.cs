using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCorrectButtonWarp : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject currentLevelWarpPrefab;

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                inCorrect_MoveToNextLevel();
            }
        }
    }

    void inCorrect_MoveToNextLevel(){
        playerPrefab.transform.position = currentLevelWarpPrefab.transform.position;
    }
}
