using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Game3Manager : MonoBehaviour
{
    [Header("InGame")]
    public bool win = false;
    public bool isGameStarted = false;
    PlayerController player;
    public Transform endGameWarpPosition;
    public GameObject tutorialUI;
    public GameObject winCanvas;

    [Header("Camera")]
    public CinemachineVirtualCamera game3Cam;
    public List<Transform> levelCam = new List<Transform>();
    public List<GameObject> levelCams = new List<GameObject>();
    public int level = 1;
    ButtonWarp buttonWarp;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        buttonWarp = FindObjectOfType<ButtonWarp>().GetComponent<ButtonWarp>();
    }

    private void FixedUpdate() {
        // SwitchCam();
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
            player.CurrentStage = GameStage.GAME3;
            StartGame();
        }
    }

    public void StartGame(){
        tutorialUI.SetActive(true);
        SwitchCam();
    }

    public void SwitchCam(){
        if(isGameStarted){
            foreach(GameObject n in levelCams){
                n.SetActive(false);
            }

            levelCams[level-1].SetActive(true);
        }
    }
}
