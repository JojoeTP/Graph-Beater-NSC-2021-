using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamBlendSetting : MonoBehaviour
{   
    public CinemachineBrain cam;
    public CinemachineVirtualCamera nextCam;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cam.ActiveBlend == null){
            nextCam.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
