using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public bool correctAnswerButtonLeft = false;
    public bool correctAnswerButtonRight = false;
    public int Game5Level = 1;

    ButtonScript[] buttonScript;

    public TMP_Text textDisplay;
    public Text buttonTextLeft;
    public Text buttonTextRight;

    public Image incorrectPic;

    private IEnumerator coroutine;
    public Button buttonLeft;
    public Button buttonRight;
    
    void Start()
    {
        buttonScript = FindObjectsOfType<ButtonScript>();
    }

    void Update()
    {
        switch(Game5Level){
            case 1:
            level1();
            break;
            case 2:
            level2();
            break;
            case 3:
            level3();
            break;
            case 4:
            level4();
            break;
        }
    }

    void level1(){
        textDisplay.SetText("6y+24=18x+42 \n6y+24=6x+48");
        buttonTextLeft.text = "/3"; //true
        buttonTextRight.text = "/5";
        foreach(ButtonScript n in buttonScript){
            n.GetComponent<ButtonScript>().correctAnswerButtonLeft = true;
            n.GetComponent<ButtonScript>().correctAnswerButtonRight = false;
        }
    }

    void level2(){
        textDisplay.SetText("2y+8=6x+14 \n2y+8=2x+16");
        buttonTextLeft.text = "*5";
        buttonTextRight.text = "-8";//true
        foreach(ButtonScript n in buttonScript){
            n.GetComponent<ButtonScript>().correctAnswerButtonLeft = false;
            n.GetComponent<ButtonScript>().correctAnswerButtonRight = true;
        }
    }

    void level3(){
        textDisplay.SetText("2y=6x+6 \n2y=2x+8");
        buttonTextLeft.text = "-7";
        buttonTextRight.text = "/2";//true
        foreach(ButtonScript n in buttonScript){
            n.GetComponent<ButtonScript>().correctAnswerButtonLeft = false;
            n.GetComponent<ButtonScript>().correctAnswerButtonRight = true;
        }
    }

    void level4(){
        textDisplay.SetText("y=3x+3 \ny=x+4");
        buttonTextLeft.text = "====";
        buttonTextRight.text = "====";
        foreach(ButtonScript n in buttonScript){
            n.GetComponent<ButtonScript>().correctAnswerButtonLeft = false;
            n.GetComponent<ButtonScript>().correctAnswerButtonRight = false;
            buttonLeft.gameObject.SetActive(false);
            buttonRight.gameObject.SetActive(false);
        }
    }
    
    public void OnClickButtonLeft(){
        if(correctAnswerButtonLeft == true){
            foreach(ButtonScript n in buttonScript){
                n.GetComponent<ButtonScript>().Game5Level += 1;
            }
        }
        else{
            incorrectPic.gameObject.SetActive(true);
            buttonLeft.enabled = false;
            buttonRight.enabled = false;

            coroutine = controlIncorrectPic(2.0f);
            StartCoroutine(coroutine);
        }
	}

    public void OnClickButtonRight(){
        if(correctAnswerButtonRight == true){
            foreach(ButtonScript n in buttonScript){
                n.GetComponent<ButtonScript>().Game5Level += 1;
            }
        }
        else{
            incorrectPic.gameObject.SetActive(true);
            buttonLeft.enabled = false;
            buttonRight.enabled = false;

            coroutine = controlIncorrectPic(2.0f);
            StartCoroutine(coroutine);
        }
	}

    private IEnumerator controlIncorrectPic(float waitTime){
        yield return new WaitForSeconds(waitTime);
        incorrectPic.gameObject.SetActive(false);
        buttonLeft.enabled = true;
        buttonRight.enabled = true;
    }
}
