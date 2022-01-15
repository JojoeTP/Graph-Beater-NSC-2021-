using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage String2Int", menuName = "Data Objects/Dictionary Storage Object S2I")]
public class DictionaryScriptableObjectS2I : ScriptableObject
{
    [SerializeField]
    List<string> keys = new List<string>();
    [SerializeField]
    List<int> values = new List<int>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<int> Values { get => values; set => values = value; }
}

