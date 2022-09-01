using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    //note that static will make this persist between scene reloads.
    public static int score;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetScore(int lastScore)
    {
        if(lastScore > score)
            score = lastScore;
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text txt = GetComponent<TMP_Text>();
        if (txt != null)
            txt.SetText("High Score: " + score.ToString());
    }
}