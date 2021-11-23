using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlLevelCam : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera game3Cam;
    public GameObject level1Cam,level2Cam,level3Cam,level4Cam,normalCam;
    ButtonWarp buttonWarp;
    
    void Start()
    {
        buttonWarp = FindObjectOfType<ButtonWarp>().GetComponent<ButtonWarp>();
        // Debug.Log(buttonWarp.level);
    }

    // Update is called once per frame
    void Update()
    {
        switch(buttonWarp.level){
            case 1:
            game3Cam.LookAt = level1Cam.transform;
            game3Cam.Follow = level1Cam.transform;
            break;
            case 2:
            game3Cam.LookAt = level2Cam.transform;
            game3Cam.Follow = level2Cam.transform;
            break;
            case 3:
            game3Cam.LookAt = level3Cam.transform;
            game3Cam.Follow = level3Cam.transform;
            break;
            case 4:
            game3Cam.LookAt = level4Cam.transform;
            game3Cam.Follow = level4Cam.transform;
            break;
        }

        // if(buttonWarp.level == 1){
        //     level1Cam.SetActive(true);
        //     level2Cam.SetActive(false);
        // }
        // if(buttonWarp.level == 2){
        //     level2Cam.SetActive(true);
        // }
        // if(buttonWarp.level == 3){
        //     level3Cam.SetActive(true);
        // }
        // if(buttonWarp.level == 4){
        //     level4Cam.SetActive(true);
        // }
        // if(buttonWarp.level == 5){
        //     level5Cam.SetActive(true);
        // }
        // if(buttonWarp.level == 6){
        //     level1Cam.SetActive(false);
        //     level2Cam.SetActive(false);
        //     level3Cam.SetActive(false);
        //     level4Cam.SetActive(false);
        //     level5Cam.SetActive(false);
        //     normalCam.SetActive(true);
        // }
        
    }
}
