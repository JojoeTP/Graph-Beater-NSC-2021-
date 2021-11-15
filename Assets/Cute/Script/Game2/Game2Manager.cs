using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MathOparation{
    PLUS,MINUS,Multiply,Divide
}

public class Game2Manager : MonoBehaviour, ISerializationCallbackReceiver
{
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

    [Header("InGame")]
    public bool isGameStarted = false;
    public int selectionChoice = 1;


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
        }

        //Dictionary
        for (int i = 0; i < Mathf.Min(dictionaryDataMathOparation.Keys.Count, dictionaryDataMathOparation.Values.Count); i++)
        {
            mathOPDictionary.Add(dictionaryDataMathOparation.Keys[i], dictionaryDataMathOparation.Values[i]);
        }
    }

    private void Start() {
        // leftSideAnswer = leftSideQuestion1;
        // rightSideAnswer = rightSideQuestion1;

        // leftSideQuestion1 = leftSideAnswer;
        // rightSideQuestion1 = rightSideAnswer;

        for(int i = 0; i < leftSideQuestion1.Count; i++){
            leftSideAnswer[i] = leftSideQuestion1[i];
            rightSideAnswer[i] = rightSideQuestion1[i];
        }
    }

    private void Update() {
        question1 = ConvertToText(leftSideQuestion1,rightSideQuestion1);
        question2 = ConvertToText(leftSideQuestion2,rightSideQuestion2);
        answer = ConvertToText(leftSideAnswer,rightSideAnswer);

        setText();
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            isGameStarted = true;
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
        for(int i = 0; i < leftSideQuestion1.Count; i++){
            leftSideAnswer[i] = leftSideQuestion1[i];
            rightSideAnswer[i] = rightSideQuestion1[i];
        }
    }

    void Plus(){
        Debug.Log("Plus");
    }
    void Minus(){
        Debug.Log("Minus");
    }
    void Multiply(){
        Debug.Log("Multiply");
    }
    void Divide(){
        Debug.Log("Divide");
    }

    //Select Choice
    public void NextChoice(){
        selectionChoice += 1;
        if(selectionChoice > numKeys.Count){
            selectionChoice = 1;
        }
    }

    public void PreviousChoice(){
        selectionChoice -= 1;
        if(selectionChoice == 0){
            selectionChoice = numKeys.Count;
        }
    }

















    //Dictionary--------------------------------------------------------------------------------------------
    public void OnBeforeSerialize()
    {
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
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void DeserializeDictionary()
    {
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

        Debug.Log("DESERIALIZATION");
        mathOPDictionary = new Dictionary<string, MathOparation>();
        dictionaryDataNumber.Keys.Clear();
        dictionaryDataNumber.Values.Clear();
        for (int i = 0; i < Mathf.Min(mathOPKeys.Count, mathOPValues.Count); i++)
        {
            dictionaryDataMathOparation.Keys.Add(mathOPKeys[i]);
            dictionaryDataMathOparation.Values.Add(mathOPValues[i]);
            mathOPDictionary.Add(mathOPKeys[i], mathOPValues[i]);
        }
        modifymathOPValues = false;
    }

    public void PrintDictionary()
    {
        foreach (var pair in NumDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }

        foreach (var pair in mathOPDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
    //Dictionary--------------------------------------------------------------------------------------------
}
