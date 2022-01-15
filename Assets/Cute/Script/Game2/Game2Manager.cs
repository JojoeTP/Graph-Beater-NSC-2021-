using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MathOparation{
    PLUS,MINUS,Multiply,Divide
}
public enum MathPosition{
    X,Y,A
}
public class Game2Manager : MonoBehaviour, ISerializationCallbackReceiver
{
    public TextMeshProUGUI choichText;
    [Header("Choice")]
    [SerializeField]
    private DictionaryScriptableObjectS2I dictionaryDataNumber;

    [SerializeField]
    private List<string> numKeys = new List<string>();
    [SerializeField]
    private List<int> numValues = new List<int>();

    private Dictionary<string, int> NumDictionary = new Dictionary<string, int>();
    public bool modifyNumValues;

    [Header("MathOparation")]
    [SerializeField]
    private DictionaryScriptableObjectS2S dictionaryDataMathOparation;

    [SerializeField]
    private List<string> mathOPKeys = new List<string>();
    [SerializeField]
    private List<MathOparation> mathOPValues = new List<MathOparation>();

    private Dictionary<string, MathOparation> mathOPDictionary = new Dictionary<string, MathOparation>();
    public bool modifymathOPValues;

    [Header("MathPosition")]
    [SerializeField]
    private DictionaryScriptableObjectS2P dictionaryDataMathPosition;

    [SerializeField]
    private List<string> mathPosKeys = new List<string>();
    [SerializeField]
    private List<MathPosition> mathPosValues = new List<MathPosition>();

    private Dictionary<string, MathPosition> mathPosDictionary = new Dictionary<string, MathPosition>();
    public bool modifymathPosValues;

    [Header("InGame")]
    public bool win = false;
    public bool isGameStarted = false;
    public int selectionChoice = 1;
    public GameObject canvas;
    public GameObject winCanvas;
    PlayerController player;


    [Header("Text")]
    public TextMeshProUGUI question1Text;
    public TextMeshProUGUI question2Text;
    public TextMeshProUGUI answerText;
    [Header("[0] = x,[1] = y,[2] = a")]
    public string Readme;
    [Header("Question1")]
    public List<int> leftSideQuestion1 = new List<int>();
    public List<int> rightSideQuestion1 = new List<int>();
    public string question1;
    [Header("Question2")]
    public List<int> leftSideQuestion2 = new List<int>();
    public List<int> rightSideQuestion2 = new List<int>();
    public string question2;
    [Header("Answer")]
    public List<int> leftSideAnswer = new List<int>();
    public List<int> rightSideAnswer = new List<int>();
    public string answer;

    private void Awake()
    {
        //Dictionary
        for (int i = 0; i < Mathf.Min(dictionaryDataNumber.Keys.Count, dictionaryDataNumber.Values.Count); i++)
        {
            NumDictionary.Add(dictionaryDataNumber.Keys[i], dictionaryDataNumber.Values[i]);
            mathOPDictionary.Add(dictionaryDataMathOparation.Keys[i], dictionaryDataMathOparation.Values[i]);
            mathPosDictionary.Add(dictionaryDataMathPosition.Keys[i], dictionaryDataMathPosition.Values[i]);
        }
    }

    private void Start() {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        for(int i = 0; i < leftSideQuestion1.Count; i++){
            leftSideAnswer[i] = leftSideQuestion1[i];
            rightSideAnswer[i] = rightSideQuestion1[i];
        }
    }

    private void Update() {
        if(!isGameStarted){
            return;
        }
        question1 = ConvertToText(leftSideQuestion1,rightSideQuestion1);
        question2 = ConvertToText(leftSideQuestion2,rightSideQuestion2);
        answer = ConvertToText(leftSideAnswer,rightSideAnswer);

        setText();
        SetChoiceText();
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            isGameStarted = true;
            player.CurrentStage = GameStage.GAME2;
            canvas.SetActive(true);
        }
    }

    public string ConvertToText(List<int> leftList,List<int> rightList){
        string equation = "";

        if(TurnIntToStringList(leftList) != "" && TurnIntToStringList(rightList) != ""){
            equation = TurnIntToStringList(leftList) + "=" + TurnIntToStringList(rightList);
        }else if(TurnIntToStringList(leftList) == "" && TurnIntToStringList(rightList) == ""){
            equation = "";
        }else if(TurnIntToStringList(leftList) == "" && TurnIntToStringList(rightList) != ""){
            equation = TurnIntToStringList(rightList);
        }else if(TurnIntToStringList(leftList) != "" && TurnIntToStringList(rightList) == ""){
            equation = TurnIntToStringList(leftList);
        }

        return equation;
    }

    public string TurnIntToStringList(List<int> _list){
        string x = "",y = "",a = "";
        string result = "";
        if(_list[0] == 0){
            x = "";
        }else if(_list[0] != 0){
            x = _list[0].ToString() + "x";
        }

        if(_list[1] == 0){
            y = "";
        }else if(_list[1] > 0){
            if(_list[0] == 0){
                y = _list[1].ToString() + "y";
            }else if(_list[0] != 0){
                y = "+" + _list[1].ToString() + "y";
            }
        }else if(_list[1] <= 0){
            y = _list[1].ToString() + "y";
        }

        if(_list[2] == 0){
            a = "";
        }else if(_list[2] > 0){
            if(_list[0] == 0 && _list[1] == 0){
                a = _list[2].ToString();
            }else if(_list[0] != 0 || _list[1] != 0){
                a = "+" + _list[2].ToString();
            }
        }else if(_list[2] <= 0){
            a = _list[2].ToString();
        }

        result = x+y+a;

        return result;
    }

    void setText(){
        question1Text.text = question1;
        question2Text.text = question2;
        answerText.text = answer;
    }

    public void EnterCalculation(){
        if(!isGameStarted){
            return;
        }

        Invoke("CheckAnswer",0.5f);
        //switch check mathOP
        switch(mathOPDictionary[selectionChoice.ToString()]){
            case MathOparation.PLUS:
                Plus();
                break;
            case MathOparation.MINUS:
                Minus();
                break;
            case MathOparation.Multiply:
                Multiply();
                break;
            case MathOparation.Divide:
                Divide();
                break;
        }
    }
    public void ResetCalculation(){
        if(!isGameStarted){
            return;
        }

        for(int i = 0; i < leftSideQuestion1.Count; i++){
            leftSideAnswer[i] = leftSideQuestion1[i];
            rightSideAnswer[i] = rightSideQuestion1[i];
        }
    }

    public void EndGame(){
        //warp to outside
        player.CurrentStage = GameStage.LOBBY;
        canvas.SetActive(false);
    }

    void CheckAnswer(){
        //check answer
        if(answer == question2){
            win = true;
            winCanvas.SetActive(true);
        }
    }

    void Plus(){
        Debug.Log("Plus");

        // for(int n = 0; n < leftSideAnswer.Count; n++){
        //     leftSideAnswer[n] += NumDictionary[selectionChoice.ToString()];
        // }

        switch(mathPosDictionary[selectionChoice.ToString()]){
            case MathPosition.X :
                leftSideAnswer[0] += NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[0] += NumDictionary[selectionChoice.ToString()];
                break;
            case MathPosition.Y :
                leftSideAnswer[1] += NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[1] += NumDictionary[selectionChoice.ToString()];
                break;
            case MathPosition.A :
                leftSideAnswer[2] += NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[2] += NumDictionary[selectionChoice.ToString()];
                break;
        }
    }
    void Minus(){
        Debug.Log("Minus");

        // for(int n = 0; n < leftSideAnswer.Count; n++){
        //     leftSideAnswer[n] -= NumDictionary[selectionChoice.ToString()];
        // }

        switch(mathPosDictionary[selectionChoice.ToString()]){
            case MathPosition.X :
                leftSideAnswer[0] -= NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[0] -= NumDictionary[selectionChoice.ToString()];
                break;
            case MathPosition.Y :
                leftSideAnswer[1] -= NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[1] -= NumDictionary[selectionChoice.ToString()];
                break;
            case MathPosition.A :
                leftSideAnswer[2] -= NumDictionary[selectionChoice.ToString()];
                rightSideAnswer[2] -= NumDictionary[selectionChoice.ToString()];
                break;
        }
    }
    void Multiply(){
        Debug.Log("Multiply");

        for(int n = 0; n < leftSideAnswer.Count; n++){
            leftSideAnswer[n] *= NumDictionary[selectionChoice.ToString()];
            rightSideAnswer[n] *= NumDictionary[selectionChoice.ToString()];
        }
    }
    void Divide(){
        Debug.Log("Divide");

        for(int n = 0; n < leftSideAnswer.Count; n++){
            leftSideAnswer[n] /= NumDictionary[selectionChoice.ToString()];
            rightSideAnswer[n] /= NumDictionary[selectionChoice.ToString()];
        }
    }

    //Select Choice
    public void NextChoice(){
        if(!isGameStarted){
            return;
        }

        selectionChoice += 1;
        if(selectionChoice > numKeys.Count){
            selectionChoice = 1;
        }
    }

    public void PreviousChoice(){
        if(!isGameStarted){
            return;
        }

        selectionChoice -= 1;
        if(selectionChoice == 0){
            selectionChoice = numKeys.Count;
        }
    }

    void SetChoiceText(){
        string mathOP = "",num = "",pos = "";

        num = NumDictionary[selectionChoice.ToString()].ToString();
        switch(mathOPDictionary[selectionChoice.ToString()]){
            case MathOparation.PLUS:
                mathOP = "+";
                break;
            case MathOparation.MINUS:
                mathOP = "-";
                break;
            case MathOparation.Multiply:
                mathOP = "*";
                break;
            case MathOparation.Divide:
                mathOP = "÷";
                break;
        }

        switch(mathPosDictionary[selectionChoice.ToString()]){
            case MathPosition.X :
                pos = "X";
                break;
            case MathPosition.Y :
                pos = "Y";
                break;
            case MathPosition.A :
                break;
        }

        choichText.text = mathOP + num + pos;
    }

    //Dictionary--------------------------------------------------------------------------------------------
    public void OnBeforeSerialize()
    {
        //MathNumber
        if (modifyNumValues == false)
        {
            numKeys.Clear();
            numValues.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryDataNumber.Keys.Count, dictionaryDataNumber.Values.Count); i++)
            {
                numKeys.Add(dictionaryDataNumber.Keys[i]);
                numValues.Add(dictionaryDataNumber.Values[i]);
            }
        }

        //MathOparation
        if (modifymathOPValues == false)
        {
            mathOPKeys.Clear();
            mathOPValues.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryDataMathOparation.Keys.Count, dictionaryDataMathOparation.Values.Count); i++)
            {
                mathOPKeys.Add(dictionaryDataMathOparation.Keys[i]);
                mathOPValues.Add(dictionaryDataMathOparation.Values[i]);
            }
        }

        //MathPosition
        if (modifymathPosValues == false)
        {
            mathPosKeys.Clear();
            mathPosValues.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryDataMathPosition.Keys.Count, dictionaryDataMathPosition.Values.Count); i++)
            {
                mathPosKeys.Add(dictionaryDataMathPosition.Keys[i]);
                mathPosValues.Add(dictionaryDataMathPosition.Values[i]);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void DeserializeDictionary()
    {
        //MathNumber
        Debug.Log("DESERIALIZATION");
        NumDictionary = new Dictionary<string, int>();
        dictionaryDataNumber.Keys.Clear();
        dictionaryDataNumber.Values.Clear();
        for (int i = 0; i < Mathf.Min(numKeys.Count, numValues.Count); i++)
        {
            dictionaryDataNumber.Keys.Add(numKeys[i]);
            dictionaryDataNumber.Values.Add(numValues[i]);
            NumDictionary.Add(numKeys[i], numValues[i]);
        }
        modifyNumValues = false;

        //MathOparation
        Debug.Log("DESERIALIZATION");
        mathOPDictionary = new Dictionary<string, MathOparation>();
        dictionaryDataMathOparation.Keys.Clear();
        dictionaryDataMathOparation.Values.Clear();
        for (int i = 0; i < Mathf.Min(mathOPKeys.Count, mathOPValues.Count); i++)
        {
            dictionaryDataMathOparation.Keys.Add(mathOPKeys[i]);
            dictionaryDataMathOparation.Values.Add(mathOPValues[i]);
            mathOPDictionary.Add(mathOPKeys[i], mathOPValues[i]);
        }
        modifymathOPValues = false;

        //MathPosition
        Debug.Log("DESERIALIZATION");
        mathPosDictionary = new Dictionary<string, MathPosition>();
        dictionaryDataMathPosition.Keys.Clear();
        dictionaryDataMathPosition.Values.Clear();
        for (int i = 0; i < Mathf.Min(mathPosKeys.Count, mathPosValues.Count); i++)
        {
            dictionaryDataMathPosition.Keys.Add(mathPosKeys[i]);
            dictionaryDataMathPosition.Values.Add(mathPosValues[i]);
            mathPosDictionary.Add(mathPosKeys[i], mathPosValues[i]);
        }
        modifymathPosValues = false;
    }

    public void PrintDictionary()
    {
        //MathNumber
        foreach (var pair in NumDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }

        //MathOparation
        foreach (var pair in mathOPDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }

        //MathPosition
        foreach (var pair in mathPosDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }

    }
    //Dictionary--------------------------------------------------------------------------------------------
}
