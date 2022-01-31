using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{   
    public GameObject pauseUI;

    private void Start() {
        pauseUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseUI.SetActive(true);         
            Time.timeScale = 0;
        }
    }

    public void Continus(){
        Time.timeScale = 1;
        pauseUI.SetActive(false);   
    }

    public void Exit(){
        SceneManager.LoadScene(0);
    }
}
