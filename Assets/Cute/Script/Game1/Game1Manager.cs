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
    PlayerController player;


    public GameObject game1AnswerUI;
    public GameObject game1Canvas;
    public GameObject winText;
    public TextMeshProUGUI XText;
    public TMP_InputField inputField;
    public float answer;
    public int _question;

    [Header("Time")]
    public float time;
    float timer;
    public Image timeImageBar;
   
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

        timer = time;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TimeCount();
    }

    public void AssignQuestionToTarget(Target _target){
        if(question.Count < 1){
            _target.SetQuestion(-1);
            return;
        }

        int random = Random.Range(0,question.Count);
        int selectQuestion = question[random];
        question.RemoveAt(random);
        _target.SetQuestion(selectQuestion);
    }

    //calculate
    public void CalculatorQuestion(){
        int x = _question;
        int y = x + 2;

        if(answer == y){
            //correct
            Debug.Log("Correct");
            correctAnswer += 1;
            HideCanvas();
        }else{
            //not correct
            Debug.Log("InCorrect");
            inCorrectAnswer += 1;
            HideCanvas();
        }

        ProgressBar();
        InCorrectBar();
    }

    public void ShowCanvasAnswer(int _x){
        _question = _x;
        XText.text = "X = " + _x;
        inputField.text = "Input Answer";
        game1AnswerUI.SetActive(true);
        player.CurrentStage = GameStage.ANSWERQUES;
    }

    public void HideCanvas(){
        game1AnswerUI.SetActive(false);
        player.CurrentStage = GameStage.GAME1;
    }

    public void SetAnswer(){
        answer = int.Parse(inputField.text);
        CalculatorQuestion();
    }

    void TimeCount(){
        timer -= 1 * Time.deltaTime;

        timeImageBar.fillAmount = timer / time;

        if(timer <= 0){
            Debug.Log("GameOver");
        }
    }

    void ProgressBar(){
        progressBar.fillAmount = (float)correctAnswer / (float)numToAnswer;

        if(correctAnswer == numToAnswer){
            Debug.Log("Win");
            win = true;
            //show Text win
            winText.SetActive(true);

            doorGame1.GetComponent<Animator>().enabled = false;
        }
    }

    void InCorrectBar(){
        inCorrectBar.fillAmount = (float)inCorrectAnswer / (float)maxInCorrectAnswer;

        if(inCorrectAnswer == maxInCorrectAnswer){
            Debug.Log("GameOver");
            isGameStarted = false;

            EndGame();
            FindObjectOfType<InstantiateGame1Manager>().GetComponent<InstantiateGame1Manager>().OnReset();

            Destroy(this.gameObject);
            
        }
    }

    public void StartGame(){
        game1Canvas.SetActive(true);
        foreach(Target n in Targets){
            n.gameObject.SetActive(true);
        }
        player.CurrentStage = GameStage.GAME1;
    }

    public void EndGame(){
        //warp to outside
        game1Canvas.SetActive(false);
        player.CurrentStage = GameStage.LOBBY;
        player.transform.position = outSidePos.position;

        
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            StartGame();
            isGameStarted = true;
        }
    }
}
