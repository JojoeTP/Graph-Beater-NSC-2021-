using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game4Manager : MonoBehaviour
{
    [Header("InGame")]
    public bool win = false;
    public bool isGameStarted = false;
    public Transform startGamePostion;
    public Transform endGameWarpPosition;
    public GameObject tutorialUI;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    PlayerController player;

    [Header("Question")]
    public float x = 0; 
    public float y = 0;
    public float a = 0;
    public string question;
    public float timeCount = 10; 
    public bool moreThan; 
    
    [Header("Setting Game")]
    public int maxPlayerHP = 3; 
    public int playerHP = 3; 
    public int amountQuestion = 10; 
    public int currentQuestion = 0;

    [Header("Line")]
    public LineRenderer line;

    [Header("UI")]
    public Canvas canvas;
    public TextMeshProUGUI Question; //Jojoe
    public Image CountdownImage; //Jojoe   
    public TextMeshProUGUI QuestionProgress; //Jojoe
    public TextMeshProUGUI Morethan; //Jojoe
    public Image Healthbar; //Jojoe    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    // Update is called once per framea
    void Update()
    {
        UpdateLine();
    }

    void FixedUpdate(){
        
    }

    public void GenerateQuestion(){
        //if win or lose don't generate question

        QuestionProgress.text = "Question Progress : " + currentQuestion + "/" + amountQuestion; //Jojoe
        //Maybe run when VFX finish
        currentQuestion++;
        if(playerHP <= 0){
            return;
        }

        if(currentQuestion > amountQuestion){
            //win
            PlayerWin();
            return;
        }

        //Generate question
        x = Random.Range(-10,10);
        y = Random.Range(1,10);
        a = Random.Range(-10,10);
        int boolean = Random.Range(0,2);
        if(boolean == 0){
            moreThan = false;
            Morethan.gameObject.SetActive(false); //Jojoe
        }else{
            moreThan = true;
            Morethan.gameObject.SetActive(true); //Jojoe
        }

        //Convert question to string and start chack position player
        ConvertQuestionToString(); //convert every time generate new question
        Question.text = question; //Jojoe
        
        StartCoroutine("CheckPlayerPosition");
        StartCoroutine("CountdownText"); //Jojoe
    }

    IEnumerator CountdownText(){ //Jojoe
        float count = timeCount;
        
        while(count>0){
            yield return new WaitForSeconds(1f);
            count--;
            float countBar = count/timeCount;
            CountdownImage.fillAmount = countBar;
        }
    }
    
    //Check player Position
    IEnumerator CheckPlayerPosition(){
            yield return new WaitForSeconds(timeCount);
            float ans = 0;
            ans = (x * player.transform.localPosition.x) + (y * player.transform.localPosition.z) + a;
            
            print(ans);//debug

            if(moreThan){
                if(ans >= 0){
                    //win this round
                    print("Win");
                }else{
                    //lose this round
                    print("Lose");
                    playerHP--;
                    if(playerHP <= 0){
                        loseCanvas.SetActive(true);
                    }
                }
            }else{
                if(ans <= 0){
                    //win this round
                    print("Win");
                }else{
                    //lose this round
                    print("Lose");
                    playerHP--;
                    if(playerHP <= 0){
                        loseCanvas.SetActive(true);
                    }
                }
            }
            float playerHealthbar = (float)playerHP/(float)maxPlayerHP; //Jojoe
            Healthbar.fillAmount = playerHealthbar; //Jojoe

            //Run next Question
            GenerateQuestion();
    }

    public void PlayerWin(){
        winCanvas.SetActive(true);
        print("Win");
    }

    public void PlayerLose(){
        //use PlayerLose() in loseCanvas Button
        print("GameOver");
        isGameStarted = false;
        player.CurrentStage = GameStage.LOBBY;
        player.transform.position = endGameWarpPosition.position;
        player.transform.parent = null;

        loseCanvas.SetActive(false);
        canvas.gameObject.SetActive(false); //Jojoe
    }

    //Convert Question To String Method
    void ConvertQuestionToString(){
        string _x = "",_y = "",_a = "";

        if(x > 0){
            _x = $"{x}x";
        }else if(x < 0){
            _x = $"{x}x";
        }else if(x == 0){
            _x = null;
        }

        if(y > 0){
            _y = $"+{y}x";
        }else if(y < 0){
            _y = $"{y}y";
        }else if(y == 0){
            _y = null;
        }

        if(a > 0){
            _a = $"+{a}";
        }else if(a < 0){
            _a = $"{a}";
        }else if(a == 0){
            _a = null;
        }

        question = $"{_x}{_y}{_a} = 0";
    }

    float CalculateX(float _X){
        float _x = 0;
        if(y == 0){
            _x = a/x;
        }else{
            _x = _X;
        }
        return _x;
    }

    float CalculateY(float _x){
        float _y = 0;
        if(y == 0){
            _y = _x;
        }else{
            _y = (-(_x * x) + (-a))/y;
        }
        return _y;
    }

    void UpdateLine(){
        for(int i = 0;i < 40; i++){
            line.SetPosition(i,new Vector3(CalculateX(i-20),CalculateY(i-20),0));
        }
    }

    //-------------------------------------------
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            if(win == true){
                return;
            }

            isGameStarted = true;
            player.CurrentStage = GameStage.GAME4;
            player.transform.parent = this.transform;
            player.transform.position = startGamePostion.position;
            StartGame();
        }
    }

    public void StartGame(){
        //Generate question and reset setting game
        playerHP = maxPlayerHP;
        currentQuestion = 0;
        tutorialUI.SetActive(true);
        canvas.gameObject.SetActive(true); //Jojoe
        //use GenerateQuestion() in button in tutorialUI
    }

    public void EndGame(){
        //Run EndGame() in winCanvas() Button 

        //button
        win = true;
        player.CurrentStage = GameStage.LOBBY;
        player.transform.position = endGameWarpPosition.position;
        player.transform.parent = null;

        winCanvas.SetActive(false);
        canvas.gameObject.SetActive(false); //Jojoe
    }
}
