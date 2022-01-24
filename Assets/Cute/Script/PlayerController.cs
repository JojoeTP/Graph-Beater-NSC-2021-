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
    public GameObject canvasPostion;
    public List<GameObject> cam = new List<GameObject>();
    public GameObject TaskUI;

    [Header("AIM")]
    Ray ray;
    RaycastHit hit;
    Vector3 target = Vector3.zero;
    public bool isAim = false;
    public GameObject game1Cam;
    public GameObject aimCam;
    public GameObject aimReticle;

    private void Awake() {
        CurrentStage = GameStage.LOBBY;
    }

    private void Update() {
        //can change to aim cam
        if(Input.GetMouseButtonDown(1)){
            isAim = true;
        }else if(Input.GetMouseButtonUp(1)){
            isAim = false;
        }    
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
        canvasPostion.SetActive(true);
        TaskUI.SetActive(true);
    }

    void Game1(){
        Move2();
        ShootRay();
        Shoot();
        canvasPostion.SetActive(false);
        TaskUI.SetActive(false);

        if(isAim && !aimCam.activeInHierarchy){
            game1Cam.SetActive(false);
            aimCam.SetActive(true);

            //Allow time for the camera to blend before enabling the UI
            StartCoroutine(ShowReticle());
        }else if(!isAim && !game1Cam.activeInHierarchy){
            game1Cam.SetActive(true);
            aimCam.SetActive(false);
            aimReticle.SetActive(false);
        }
        
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }

    void Game2(){
        canvasPostion.SetActive(false);
        TaskUI.SetActive(false);
    }
    void Game3(){
        Move1();
        canvasPostion.SetActive(false);
        TaskUI.SetActive(false);
    }

    void Game4(){
        Move1();
        canvasPostion.SetActive(false);
        TaskUI.SetActive(false);
    }
    void Game5(){
        canvasPostion.SetActive(false);
        TaskUI.SetActive(false);
    }

    void FinalGame(){
        canvasPostion.SetActive(false);
    }
    void AnswerQues(){
        //don't MOVE
        canvasPostion.SetActive(false);
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
            GameObject bullet = Instantiate(bulletPrefab,gunBarrel.position,Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<BulletMove>().target = target;
            Debug.Log("SHOOT!!");
        }
    }

    public void ChangeCamera(int _cam){
        foreach(GameObject n in cam){
            n.SetActive(false);
        }
        
        cam[_cam].SetActive(true);
    }

    public void ShootRay(){
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        
        if (Physics.Raycast(ray, out hit)){
            target = hit.point;
        }else{
            target = Vector3.zero;
        }
    }

    private void OnDrawGizmos() {
            Gizmos.DrawRay(ray);
    }
}
