using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionText : MonoBehaviour
{
    public TextMeshProUGUI positionTextPrefab;
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
}