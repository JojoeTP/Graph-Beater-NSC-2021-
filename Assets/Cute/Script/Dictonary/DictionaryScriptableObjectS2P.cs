using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage String2Position", menuName = "Data Objects/Dictionary Storage Object S2P")]
public class DictionaryScriptableObjectS2P : ScriptableObject
{
    [SerializeField]
    List<string> keys = new List<string>();
    [SerializeField]
    List<MathPosition> values = new List<MathPosition>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<MathPosition> Values { get => values; set => values = value; }
}