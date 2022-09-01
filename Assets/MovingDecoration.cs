using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDecoration : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    FinishLine finishScript;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(15 + Random.Range(1f, 15f), transform.position.y, transform.position.z);
        transform.position += Vector3.forward * Random.Range(2.4f, 6f);
        if (moveSpeed <= 3)
        {
            moveSpeed = Random.Range(1.2f, 2.6f);
            
            //removed but left commented out for future reference
            //transform.localScale += new Vector3(Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), 1f);
        }

        finishScript = GameObject.FindObjectOfType<FinishLine>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed * (1.0f + (Time.timeSinceLevelLoad * 0.05f));

        if (transform.position.x < -12f && finishScript.bCanRespawn)
        {
            transform.position = new Vector3(15 + Random.Range(1f, 15f), transform.position.y, transform.position.z);

            //removed but left commented out for future reference
            //transform.localScale += new Vector3(Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), 1f);
        }
    }
}
