using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStage {LOBBY,GAME1,GAME2,GAME3,GAME4,GAME5,FINALGAME,ANSWERQUES};
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    Vector3 diraction;
    public Transform camFollow;
    public GameStage CurrentStage = GameStage.LOBBY;
    public GameObject bulletPrefab;
    public Transform gunBarrel;

    public List<GameObject> cam = new List<GameObject>();

    private void Awake() {
        CurrentStage = GameStage.LOBBY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (CurrentStage){
            case GameStage.LOBBY :
            Lobby();
            ChangeCamera(0);
            break;
            case GameStage.GAME1 :
            Game1();
            ChangeCamera(1);
            break;
            case GameStage.GAME2 :
            Game2();
            ChangeCamera(2);
            break;
            case GameStage.GAME3 :
            Game3();
            ChangeCamera(3);
            break;
            case GameStage.GAME4 :
            Game4();
            ChangeCamera(4);
            break;
            case GameStage.GAME5 :
            Game5();
            ChangeCamera(5);
            break;
            case GameStage.FINALGAME :
            FinalGame();
            ChangeCamera(6);
            break;
            case GameStage.ANSWERQUES :
            AnswerQues();
            break;
        }
    }

    

    void Lobby(){
        Move1();
    }

    void Game1(){
        Move2();
        Shoot();
    }

    void Game2(){

    }
    void Game3(){
        Move1();
    }

    void Game4(){

    }
    void Game5(){

    }

    void FinalGame(){

    }
    void AnswerQues(){
        //don't MOVE
    }

    //Player move 1
    void Move1(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        diraction = new Vector3(horizontal,0,vertical);

        if(diraction.magnitude >= 0.1f){
            transform.rotation = Quaternion.LookRotation(diraction);
            // transform.Translate(diraction * moveSpeed * Time.deltaTime);
            transform.position += diraction * moveSpeed * Time.deltaTime;
            camFollow.position = transform.position;
        }
    }
    void Move2(){
        float horizontal = Input.GetAxisRaw("Horizontal");

        diraction = new Vector3(horizontal,0,0);

        if(diraction.magnitude >= 0.1f){
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.position += diraction * moveSpeed * Time.deltaTime;
            camFollow.position = transform.position;
        }
    }

    //Shooting for game1
    void Shoot(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bulletPrefab,gunBarrel.position,Quaternion.Euler(Vector3.zero));
            Debug.Log("SHOOT!!");
        }
    }

    void ChangeCamera(int _cam){
        foreach(GameObject n in cam){
            n.SetActive(false);
        }
        
        cam[_cam].SetActive(true);
    }
}
