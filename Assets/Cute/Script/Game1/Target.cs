using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    Game1Manager game1Manager;
    public TextMeshProUGUI questionText;
    int question = 0;

    [Range(0,1)]
    public float lerpValue = 0;

    public float countShowTime = 2;

    private void Start() {
        game1Manager = FindObjectOfType<Game1Manager>().GetComponent<Game1Manager>();
        
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90f,180,0f),Quaternion.Euler(0f,180,0f),0f);
        StartCoroutine(ShowTarget());
    }
    private void FixedUpdate() {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90f,180,0f),Quaternion.Euler(0f,180,0f),lerpValue);
    }

    //show canvas
    void ShowBoarder(){

    }

    IEnumerator ShowTarget(){
        
        yield return new WaitForSeconds(2f);
        game1Manager.AssignQuestionToTarget(this);
        this.GetComponent<Collider>().enabled = true;
        StartCoroutine(IEShowTarget());

    }

    public void HideTarget(){
        StartCoroutine(IEHideTarget());
    }
    
    public void SetQuestion(int _question){
        question = _question;
        if(question == -1){
            questionText.text = null;
            return;
        }

        questionText.text = question.ToString();
    }

    IEnumerator IEHideTarget(){
        this.GetComponent<Collider>().enabled = false;

        while(lerpValue > 0){
            lerpValue -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ShowTarget()); //Move to when answer the question finish
    }

    IEnumerator IEShowTarget(){
        while(lerpValue < 1){
            lerpValue += Time.deltaTime;
            yield return null;
        }
    }
}
