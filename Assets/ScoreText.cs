using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public int score;
    private TMP_Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
        ResetScore();
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (txt != null)
        {
            txt.SetText("Score: " + score.ToString());
        }
    }
}
