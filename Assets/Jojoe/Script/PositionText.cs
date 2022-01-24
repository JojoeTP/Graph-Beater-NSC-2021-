using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionText : MonoBehaviour
{
    public TextMeshProUGUI positionTextPrefab;
    bool setActivePositionCanvas = true;
    Transform playerPosition;
    public GameObject playerPrefab;

    float posXFloat; int posXInt;  string posXString;
    float posZFloat; int posZInt;  string posZString;

    void Start()
    {
        playerPosition = playerPrefab.GetComponent<Transform>();
    }

    void Update() {
        OnPositionChange();
        press_H_toHideText();
    }

    void OnPositionChange(){
        posXFloat = playerPosition.transform.position.x;
        posXInt = (int)posXFloat / 2;
        posXString = posXInt.ToString();

        posZFloat = playerPosition.transform.position.z;
        posZInt = (int)posZFloat / 2;
        posZString = posZInt.ToString();
        positionTextPrefab.text = posXString + "," + posZString;
    }

    void press_H_toHideText(){
        if(Input.GetKeyDown(KeyCode.H)){
            if(setActivePositionCanvas == true){
                setActivePositionCanvas = false;
            }
            else{
                setActivePositionCanvas = true;
            }
        }
        positionTextPrefab.gameObject.SetActive(setActivePositionCanvas);
    }
}