using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game2Manager : MonoBehaviour, ISerializationCallbackReceiver
{
    [Header("Choice")]
    [SerializeField]
    private DictionaryScriptableObject dictionaryDataNumber;

    [SerializeField]
    private List<string> numKeys = new List<string>();
    [SerializeField]
    private List<int> numValues = new List<int>();

    private Dictionary<string, int> NumDictionary = new Dictionary<string, int>();
    public bool modifyNumValues;

    [Header("MathOparation")]
    [SerializeField]
    private DictionaryScriptableObject dictionaryDataMathOparation;

    [SerializeField]
    private List<string> mathOPKeys = new List<string>();
    [SerializeField]
    private List<int> mathOPValues = new List<int>();

    private Dictionary<string, int> mathOPDictionary = new Dictionary<string, int>();
    public bool modifymathOPValues;


    public bool isGameStarted = false;
    [Header("Text")]
    public TextMeshProUGUI question1;
    public TextMeshProUGUI question2;
    public TextMeshProUGUI answer;
    [Header("LeftSide")]
    public int x_L = 0;
    public int y_L = 0;
    public int a_L = 0;
    [Header("RightSide")]
    public int x_R = 0;
    public int y_R = 0;
    public int a_R = 0;
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

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isGameStarted){
            //Play game2
            isGameStarted = true;
        }
    }

    public void ConvertToText(){
        
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
        mathOPDictionary = new Dictionary<string, int>();
        dictionaryDataNumber.Keys.Clear();
        dictionaryDataNumber.Values.Clear();
        for (int i = 0; i < Mathf.Min(mathOPKeys.Count, mathOPValues.Count); i++)
        {
            dictionaryDataNumber.Keys.Add(mathOPKeys[i]);
            dictionaryDataNumber.Values.Add(mathOPValues[i]);
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
