using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game1Manager : MonoBehaviour
{
    public bool isGameStarted = false;
    public bool win = false;
    public Transform outSidePos;
    public DoorScript doorGame1;
    public List<Target> Targets = new List<Target>();
    public List<int> question = new List<int>();
    public List<int> currentQuestion = new List<int>();
    PlayerController player;


    public GameObject game1AnswerUI;
    public GameObject game1Canvas;
    public TextMeshProUGUI XText;
    // public TMP_InputField inputField;
    // public float answer;
    // public int _question;

    public GameObject tutorialUI;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    [Header("Question")]
    public int x = 0;
    public int y = 0;
    public int a = 0;

    // [Header("Time")]
    // public float time;
    // float timer;
    // public Image timeImageBar;
   
    [Header("Progress")]
    public int numToAnswer;
    public int correctAnswer = 0;
    public Image progressBar;

    [Header("Incorrect")]
    public int maxInCorrectAnswer;
    public int inCorrectAnswer = 0;
    public Image inCorrectBar;
    // Target[] _targets;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        // _targets = FindObjectsOfType<Target>();
        // AssignQuestionToAllTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateBar();
        // TimeCount();
    }

    public void AssignQuestionToTarget(Target _target){
        if(currentQuestion.Count < 1){
            _target.SetQuestion(-1);
            return;
        }

        int random = Random.Range(0,currentQuestion.Count);
        int selectQuestion = currentQuestion[random];
        currentQuestion.RemoveAt(random);
        _target.SetQuestion(selectQuestion);
    }

    //calculate when hit sub target
    public void CalculatorQuestion(float question,float answer){
        float _x = question;
        float _y = ((_x * x) + a) / y; //change this if you want to change question

        if(answer == _y){
            //correct
            Debug.Log("Correct");
            correctAnswer += 1;
            // HideCanvas();
        }else{
            //not correct
            Debug.Log("InCorrect");
            inCorrectAnswer += 1;
            // HideCanvas();
        }

        ProgressBar();
        InCorrectBar();
    }

    public float FindAnswer(float question){
        float _y = question + 2;
        return _y;
    }

    //Don't use it any more!
    // public void ShowCanvasAnswer(int _x){
    //     _question = _x;
    //     XText.text = "X = " + _x;
    //     inputField.text = "Input Answer";
    //     game1AnswerUI.SetActive(true);
    //     player.CurrentStage = GameStage.ANSWERQUES;
    // }

    //Don't use it any more!
    // public void HideCanvas(){
    //     game1AnswerUI.SetActive(false);
    //     player.CurrentStage = GameStage.GAME1;
    // }

    //Don't use it any more!
    // public void SetAnswer(){
    //     answer = int.Parse(inputField.text);
    //     // CalculatorQuestion();
    //     foreach(Target n in Targets){
    //         if(n.lerpValue < 1){
    //             n.ShowTarget(); //Move Target to when answer the question finish
    //         }
    //     }
    // }

    //TimeCount
    // void TimeCount(){
    //     timer -= 1 * Time.deltaTime;

    //     timeImageBar.fillAmount = timer / time;

    //     if(timer <= 0){
    //         Debug.Log("GameOver");
    //     }
    // }

    void UpdateBar(){
        progressBar.fillAmount = (float)correctAnswer / (float)numToAnswer;
        inCorrectBar.fillAmount = (float)inCorrectAnswer / (float)maxInCorrectAnswer;
    }

    void ProgressBar(){
        if(correctAnswer == numToAnswer){
            Debug.Log("Win");
            win = true;
            //show Text win
            winCanvas.SetActive(true);

            doorGame1.GetComponent<Animator>().enabled = false;
        }
    }

    void InCorrectBar(){
        if(inCorrectAnswer == maxInCorrectAnswer){
            Debug.Log("GameOver");
            isGameStarted = false;
            loseCanvas.SetActive(true);
            // EndGame();
            // FindObjectOfType<InstantiateGame1Manager>().GetComponent<InstantiateGame1Manager>().OnReset();

            // Destroy(this.gameObject);
            
        }
    }

    public void StartGame(){
        // timer = time;
        inCorrectAnswer = 0;
        correctAnswer = 0;

        if(currentQuestion != null){
            currentQuestion.RemoveRange(0,currentQuestion.Count);
        }

        foreach(int n in question){
            currentQuestion.Add(n);
        }

        game1Canvas.SetActive(true);
        tutorialUI.SetActive(true);
    }

    public void ActiveTargetAndRandomQuestion(){
        //use ActiveTargetAndRandomQuestion() in button in tutorialUI
        foreach(Target n in Targets){
            n.gameObject.SetActive(true);
            n.ShowTarget();
        }

        //And !!!!! Don't forget to random question
    }

    public void EndGame(){
        //warp to outside
        game1Canvas.SetActive(false);
        player.CurrentStage = GameStage.LOBBY;
        player.transform.position = outSidePos.position;

        //Hide all target
        foreach(Target n in Targets){
            n.GetComponent<Target>().HideTargetWhenEndGame();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            isGameStarted = true;
            player.CurrentStage = GameStage.GAME1;
            player.ChangeCamera(1);
            StartGame();
        }
    }
}
