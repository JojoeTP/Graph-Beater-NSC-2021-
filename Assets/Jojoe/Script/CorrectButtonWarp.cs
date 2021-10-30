using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectButtonWarp : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject nextLevelWarpPrefab;

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.E)){
                correct_MoveToNextLevel();
            }
        }
    }

    void correct_MoveToNextLevel(){
        playerPrefab.transform.position = nextLevelWarpPrefab.transform.position;
    }
}
