using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game4Manager : MonoBehaviour
{
    [Header("Question")]
    public float x = 0; 
    public bool xPow2 = false;
    public float y = 0;
    public bool yPow2 = false;
    public float a = 0;
    public string question;

    [Header("Line")]
    public LineRenderer line;
    

    // Start is called before the first frame update
    void Start()
    {
        ConvertQuestionToString();
    }

    // Update is called once per framea
    void Update()
    {
        for(int i = 0;i < 40; i++){
            line.SetPosition(i,new Vector3(CalculateX(i-20),CalculateY(i-20),0));
        }
    }

    void ConvertQuestionToString(){
        string _x = "",_y = "",_a = "";

        if(x > 0){
            if(xPow2){
                _x = $"{x}x²";
            }else{
                _x = $"{x}x";
            }
            
        }else if(x < 0){
            if(xPow2){
                _x = $"{x}x²";
            }else{
                _x = $"{x}x";
            }
        }else if(x == 0){
            _x = null;
        }

        if(y > 0){
            if(yPow2){
                _y = $"+{y}x²";
            }else{
                _y = $"+{y}x";
            }
        }else if(y < 0){
            if(yPow2){
                _y = $"{y}x²";
            }else{
                _y = $"{y}x";
            }
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

        question = $"{_x}{_y}{_a}";
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
            // _y = (-(x * (xPow2 ? x : 1)) + (-a))/y;
            _y = (-(_x * x) + (-a))/y;
        }
        return _y;
    }
}
