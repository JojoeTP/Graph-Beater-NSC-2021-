using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private DictionaryScriptableObjectS2I dictionaryDataS2I;

    [SerializeField]
    private List<string> keys = new List<string>();
    [SerializeField]
    private List<int> values = new List<int>();

    private Dictionary<string, int> myDictionary = new Dictionary<string, int>();

    public bool modifyValues;

    private void Awake()
    {
        for (int i = 0; i < Mathf.Min(dictionaryDataS2I.Keys.Count, dictionaryDataS2I.Values.Count); i++)
        {
            myDictionary.Add(dictionaryDataS2I.Keys[i], dictionaryDataS2I.Values[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        if (modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryDataS2I.Keys.Count, dictionaryDataS2I.Values.Count); i++)
            {
                keys.Add(dictionaryDataS2I.Keys[i]);
                values.Add(dictionaryDataS2I.Values[i]);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void DeserializeDictionary()
    {
        Debug.Log("DESERIALIZATION");
        myDictionary = new Dictionary<string, int>();
        dictionaryDataS2I.Keys.Clear();
        dictionaryDataS2I.Values.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryDataS2I.Keys.Add(keys[i]);
            dictionaryDataS2I.Values.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void PrintDictionary()
    {
        foreach (var pair in myDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
}
