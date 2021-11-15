using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlLevelCam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject level1Cam,level2Cam,level3Cam,level4Cam,level5Cam,normalCam;
    ButtonWarp buttonWarp;
    
    void Start()
    {
        buttonWarp = FindObjectOfType<ButtonWarp>().GetComponent<ButtonWarp>();
        Debug.Log(buttonWarp.level);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(buttonWarp.level == 1){
            level1Cam.SetActive(true);
            level2Cam.SetActive(false);
        }
        if(buttonWarp.level == 2){
            level2Cam.SetActive(true);
        }
        if(buttonWarp.level == 3){
            level3Cam.SetActive(true);
        }
        if(buttonWarp.level == 4){
            level4Cam.SetActive(true);
        }
        if(buttonWarp.level == 5){
            level5Cam.SetActive(true);
        }
        if(buttonWarp.level == 6){
            level1Cam.SetActive(false);
            level2Cam.SetActive(false);
            level3Cam.SetActive(false);
            level4Cam.SetActive(false);
            level5Cam.SetActive(false);
            normalCam.SetActive(true);
        }
        
    }
}
