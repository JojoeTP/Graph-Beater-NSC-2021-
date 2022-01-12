using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Manager : MonoBehaviour
{
    [Header("InGame")]
    public bool win = false;
    public bool isGameStarted = false;
    PlayerController player;
    public Transform endGameWarpPosition;
    public GameObject winCanvas;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    public void EndGame(){
        //button
        win = true;
        player.CurrentStage = GameStage.LOBBY;
        player.transform.position = endGameWarpPosition.transform.position;

        winCanvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            isGameStarted = true;
            player.CurrentStage = GameStage.GAME4;
        }
    }
}
