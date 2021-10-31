using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game1Manager : MonoBehaviour
{
    public List<int> question = new List<int>();
    // Target[] _targets;
    

    // Start is called before the first frame update
    void Start()
    {
        // _targets = FindObjectsOfType<Target>();
        // AssignQuestionToAllTarget();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void AssignQuestionToAllTarget(){
    //     for(int i = 0; i < _targets.Length; i++){
    //         int random = Random.Range(0,question.Count);
    //         int selectQuestion = question[random];
    //         question.RemoveAt(random);

    //         _targets[i].SetQuestion(selectQuestion);
    //     }
    // }

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
    void CalculatorQuestion(int _x, int answer){
        int x = _x;
        int y = x + 2;

        if(answer == y){
            //correct
        }else{
            //not correct
        }
    }
}
