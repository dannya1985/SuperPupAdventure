using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillOnTouch : MonoBehaviour
{
    private GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = GameObject.Find("GameOver");
        gameOver.GetComponent<Canvas>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            HighScoreText htxt = FindObjectOfType<HighScoreText>();
            ScoreText txt = FindObjectOfType<ScoreText>();

            if (htxt != null && txt != null)
            {
                htxt.SetScore(txt.score);
            }

            Time.timeScale = 0;
            AudioListener.pause = true;
            gameOver.GetComponent<Canvas>().enabled = true;
        }
    }
}
