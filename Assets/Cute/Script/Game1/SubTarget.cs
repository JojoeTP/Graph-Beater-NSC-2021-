using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubTarget : MonoBehaviour
{
    bool isCorrect = false; 
    public float answer = 0;
    public Target target;
    public TextMeshProUGUI ansText;
    

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.parent.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        ansText.text = answer.ToString("0.00");
    }

    public void RandomAnswer(){
        answer = Random.Range(1f,20f);
    }
}
