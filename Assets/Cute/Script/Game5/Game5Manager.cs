using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestionGame5{
    public string question;
    public string answer1;
    public bool answer1Correct;
    public string answer2;
    public bool answer2Correct;
}

public class Game5Manager : MonoBehaviour
{
    [Header("InGame")]
    public bool win = false;
    public bool isGameStarted = false;
    PlayerController player;
    public GameObject tutorialUI;
    public GameObject winCanvas;
    [Header("Question")]
    public int currentQuestion = 0;

    [Header("Question Setting")]
    public List<QuestionGame5> question = new List<QuestionGame5>();

    [Header ("Question Text Setting")]
    public GameObject gameCanvas;
    public TextMeshProUGUI textDisplay;
    public Text buttonTextLeft;
    public Text buttonTextRight;
    [Header("Image")]
    public GameObject incorrectPic;
    [Header("Button")]
    public Button buttonLeft;
    public Button buttonRight;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void FixedUpdate() {

    }

    public void EndGame(){
        //button
        win = true;
        player.CurrentStage = GameStage.LOBBY;

        winCanvas.SetActive(false);
        gameCanvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            isGameStarted = true;
            player.CurrentStage = GameStage.GAME5;
            StartGame();
        }
    }

    public void StartGame(){
        tutorialUI.SetActive(true);
        gameCanvas.SetActive(true);
    }

    public void SwitchQuestion(){
        //first run this on tutorial button and run this when answer correct

        if(currentQuestion == question.Count){
            winCanvas.SetActive(true);
            return;
        }

        textDisplay.text = question[currentQuestion].question;
        buttonTextLeft.text = question[currentQuestion].answer1;
        buttonTextRight.text = question[currentQuestion].answer2;
    }

    public void OnCliclLeftButton(){
        if(question[currentQuestion].answer1Correct){
            currentQuestion++;

            //and run switch question;
            SwitchQuestion();
        }
        else{
            incorrectPic.SetActive(true);
            buttonLeft.enabled = false;
            buttonRight.enabled = false;

            StartCoroutine(controlIncorrectPic(2.0f));
        }
    }

    public void OnClickRightButton(){
        if(question[currentQuestion].answer2Correct){
            currentQuestion++;
            SwitchQuestion();
        }
        else{
            incorrectPic.SetActive(true);
            buttonLeft.enabled = false;
            buttonRight.enabled = false;

            StartCoroutine(controlIncorrectPic(2.0f));
        }
    }

    private IEnumerator controlIncorrectPic(float waitTime){
        yield return new WaitForSeconds(waitTime);
        incorrectPic.gameObject.SetActive(false);
        buttonLeft.enabled = true;
        buttonRight.enabled = true;
    }

    
}

