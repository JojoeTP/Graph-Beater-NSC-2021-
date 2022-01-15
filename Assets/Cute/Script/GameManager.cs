using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Game1Manager game1Manager;
    public Game2Manager game2Manager;
    public Game3Manager game3Manager;
    public Game4Manager game4Manager;
    public Game5Manager game5Manager;
    [Header("Text")]

    public TextMeshProUGUI game1;
    public TextMeshProUGUI game2,game3,game4,game5;

    // Start is called before the first frame update
    void Start()
    {
        game1Manager = FindObjectOfType<Game1Manager>().GetComponent<Game1Manager>();
        game2Manager = FindObjectOfType<Game2Manager>().GetComponent<Game2Manager>();
        game3Manager = FindObjectOfType<Game3Manager>().GetComponent<Game3Manager>();
        game4Manager = FindObjectOfType<Game4Manager>().GetComponent<Game4Manager>();
        game5Manager = FindObjectOfType<Game5Manager>().GetComponent<Game5Manager>();

        game5Manager.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UnlockLastGame();
        UpdateText();
    }
    
    void UnlockLastGame(){
        if(game1Manager.win && game2Manager.win && game3Manager.win  && game4Manager.win){
            game5Manager.gameObject.SetActive(true);
            game5.gameObject.SetActive(true);
        }
    }

    void UpdateText(){
        game1.text = "GAME1 (" + (int)game1Manager.transform.localPosition.x/2 + "," + (int)game1Manager.transform.localPosition.z/2 + ")";
        game2.text = "GAME2 (" + (int)game2Manager.transform.localPosition.x/2 + "," + (int)game2Manager.transform.localPosition.z/2 + ")";
        game3.text = "GAME3 (" + (int)game3Manager.transform.localPosition.x/2 + "," + (int)game3Manager.transform.localPosition.z/2 + ")";
        game4.text = "GAME4 (" + (int)game4Manager.transform.localPosition.x/2 + "," + (int)game4Manager.transform.localPosition.z/2 + ")";
        game5.text = "GAME5 (" + (int)game5Manager.transform.localPosition.x/2 + "," + (int)game5Manager.transform.localPosition.z/2 + ")";

        if(game1Manager.win){
            game1.fontStyle = FontStyles.Strikethrough;
        }
        if(game2Manager.win){
            game2.fontStyle = FontStyles.Strikethrough;
        }
        if(game3Manager.win){
            game3.fontStyle = FontStyles.Strikethrough;
        }
        if(game4Manager.win){
            game4.fontStyle = FontStyles.Strikethrough;
        }
        if(game5Manager.win){
            game5.fontStyle = FontStyles.Strikethrough;
        }

    }
}
