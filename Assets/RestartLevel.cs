using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = GameObject.Find("GameOver");
        gameOver.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        gameOver.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitSceneButtonPress()
    {
        AudioListener.pause = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}