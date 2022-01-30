using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    Game1Manager game1Manager;
    public TextMeshProUGUI questionText;
    float question = 0;
    float answer = 0;

    [Range(0,1)]
    public float lerpValue = 0;

    public float countShowTime = 2;

    public List<SubTarget> subTargets = new List<SubTarget>();

    private void Start() {
        game1Manager = FindObjectOfType<Game1Manager>().GetComponent<Game1Manager>();
        
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90f,180,0f),Quaternion.Euler(0f,180,0f),0f);
    }
    private void FixedUpdate() {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90f,180,0f),Quaternion.Euler(0f,180,0f),lerpValue);
    }

    public void ShowTarget(){
        StartCoroutine(AssignToTarget());
    }

    IEnumerator AssignToTarget(){
        yield return new WaitForSeconds(2f);
        game1Manager.AssignQuestionToTarget(this);
        if(question > 0){
            this.GetComponent<Collider>().enabled = true;
            StartCoroutine(IEShowTarget());
        }
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
        ShowTarget(); //Move to when answer the question finish
    }

    public void HideTargetWhenEndGame(){
        StopAllCoroutines();
        lerpValue = 0;
    }

    IEnumerator IEShowTarget(){
        while(lerpValue < 1){
            lerpValue += Time.deltaTime;
            yield return null;
        }
    }

    //Sub target
    public void ShowSubTarget(){
        //Random answer in on subTarget

        foreach(SubTarget n in subTargets){
            n.gameObject.SetActive(true);
            n.RandomAnswer();
        }

        answer = game1Manager.FindAnswer(question);
        SetAnswerToRandomSubTarget();
    }

    void SetAnswerToRandomSubTarget(){
        int random = Random.Range(0,subTargets.Count);
        subTargets[random].answer = answer;
    }

    //calculate on this funtion
    public void HideSubTarget(float answer){
        game1Manager.CalculatorQuestion(question,answer);

        foreach(SubTarget n in subTargets){
            n.gameObject.SetActive(false);
        }
    }
    
}
