using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] public bool bCanRespawn = true;

    private float startingYPosition;

    private Time startTime;

    Player player;
    FinishLine finishScript;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText txt = FindObjectOfType<ScoreText>();

        if (txt != null)
        {
            txt.ResetScore();
        }

        startingYPosition = transform.position.y;

        randomizeColor();

        float newY = startingYPosition + Random.Range(-1f, 1f);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();

        finishScript = GameObject.FindObjectOfType<FinishLine>();

        if (PlayerPrefs.GetString("difficulty") == "easy")
            moveSpeed = 1.5f;
        else if (PlayerPrefs.GetString("difficulty") == "normal")
            moveSpeed = 2.5f;
        else
            moveSpeed = 3f;
    }

    void randomizeColor()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.bInvincible)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        transform.position += Vector3.left * Time.deltaTime * moveSpeed * (1.0f + (Time.timeSinceLevelLoad * 0.05f));

        if (transform.position.x < -18f && finishScript.bCanRespawn)
        {
            transform.position += Vector3.right * 30;

            float newY = startingYPosition + Random.Range(-1f, 1f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            ScoreText txt = FindObjectOfType<ScoreText>();

            if (txt != null)
            {
                txt.IncrementScore();
            }

            randomizeColor();
        }
    }
}
