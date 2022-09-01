using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float respawnTimeSeconds = 5f;
    [SerializeField] private float maxYmoveRange = 0.55f;

    private bool bHasCollided;
    private float tLastCollision;
    private Player player;
    private Vector3 spawnedPostion;
    private bool bMoveUpwards = true;
    FinishLine finishScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();
        finishScript = GameObject.FindObjectOfType<FinishLine>();

        if (PlayerPrefs.GetString("difficulty") == "easy")
        {
            moveSpeed = 1.5f;
        }
        else if (PlayerPrefs.GetString("difficulty") == "normal")
        {
            moveSpeed = 2.5f;
        }
        else
        {
            moveSpeed = 3f;
        }

        Reset();
    }

    void Reset()
    {
        //enable drawing
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        //enable collisions
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        bHasCollided = false;
        spawnedPostion = new Vector3(31, Random.Range(-4f, 4f));
        transform.position = spawnedPostion;

        if (player != null)
        {
            player.bInvincible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move to the left of the screen
        transform.position += Vector3.left * Time.deltaTime * moveSpeed * (1.0f + (Time.timeSinceLevelLoad * 0.05f));

        //bob up and down inside the set range
        //up
        if (transform.position.y < (spawnedPostion.y + maxYmoveRange) && bMoveUpwards)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed * 0.6f;
        }
        else if (transform.position.y >= (spawnedPostion.y + maxYmoveRange) && bMoveUpwards)
        {
            bMoveUpwards = false;
        }

        //down
        if (transform.position.y > (spawnedPostion.y - maxYmoveRange) && !bMoveUpwards)
        {
            transform.position += Vector3.down * Time.deltaTime * moveSpeed * 0.6f;
        }
        else if (transform.position.y <= (spawnedPostion.y - maxYmoveRange) && !bMoveUpwards)
        {
            bMoveUpwards = true;
        }

        //OnCollisionEnter2D (below) checks for the collision and sets bHascollided
        //if it's set to true, reset the powerup so it respawns and back to its initial state
        if (bHasCollided && finishScript.bCanRespawn)
        {
            if (Time.time - tLastCollision > respawnTimeSeconds)
            {
                Reset();
            }
        }

        //respawn and reset to initial state if it moves off of the screen
        if (transform.position.x < -20f && finishScript.bCanRespawn)
        {
            Reset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();

            //disable drawing the powerup
            GetComponent<SpriteRenderer>().enabled = false;

            //disable collisions
            GetComponent<BoxCollider2D>().enabled = false;

            //Save the time we collided to track the countdown
            tLastCollision = Time.time;

            //flag to reset state
            bHasCollided = true;

            //tell the player they are invincible
            if (player != null)
            {
                player.bInvincible = true;
            }
        }
    }
}
