using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private ScoreText scoretxt;
    public bool bCanRespawn = true;
    private GameObject gameWin;
    private GameObject gameWinMenu;
    private GameObject gameOver;
    private float moveSpeed = 3f;
    private Player player;
    private float timeSinceWin;
    [SerializeField] public float winScore;

    // Start is called before the first frame update
    void Start()
    {
        bCanRespawn = true;
        scoretxt = GameObject.FindObjectOfType<ScoreText>();

        gameWin = GameObject.Find("GameWin");
        gameWin.transform.position = new Vector3(-20f, 0f, 0f);
        gameWinMenu = GameObject.Find("GameWinMenu");
        gameWinMenu.GetComponent<Canvas>().enabled = false;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();
        gameWinMenu = GameObject.Find("GameWinMenu");
        timeSinceWin = 0f;

        if (PlayerPrefs.GetString("difficulty") == "easy")
        {
            winScore = 25f;
            moveSpeed = 1.5f;
        }
        else if (PlayerPrefs.GetString("difficulty") == "normal")
        {
            winScore = 125f;
            moveSpeed = 2.5f;
        }
        else
        {
            winScore = 200f;
            moveSpeed = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoretxt.score > winScore)
        {
            bCanRespawn = false;
            player.bInvincible = false;

            if (transform.position.x > 4)
            {
                transform.position += Vector3.left * Time.deltaTime * moveSpeed * (1.0f + (Time.timeSinceLevelLoad * 0.05f));
                timeSinceWin = 0f;
            }
            else
            {
                //move the 'yeah' back on-screen
                gameWin.transform.position = new Vector3(8.8f, 3f, 0.3f);

                timeSinceWin += Time.deltaTime;

                AudioListener.pause = true;

                //wait 5 seconds until showing the game win menu
                if(timeSinceWin > 5f)
                {
                    gameWinMenu.GetComponent<Canvas>().enabled = true;
                }
            }
        }
    }
}
