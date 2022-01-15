using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGame1Manager : MonoBehaviour
{
    public GameObject game1Prefab;

    public void OnReset(){
        Instantiate(game1Prefab);
    }
}
