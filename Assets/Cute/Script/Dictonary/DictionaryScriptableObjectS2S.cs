using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage String2String", menuName = "Data Objects/Dictionary Storage Object S2S")]
public class DictionaryScriptableObjectS2S : ScriptableObject
{
    [SerializeField]
    List<string> keys = new List<string>();
    [SerializeField]
    List<MathOparation> values = new List<MathOparation>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<MathOparation> Values { get => values; set => values = value; }
}