using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDecorationBig : MonoBehaviour
{
    [SerializeField] private float moveSpeed        = 2f;

    [SerializeField] private float maxRandomScaling = 0.2f;
    [SerializeField] private float minRandomScaling = 0.0f;

    [SerializeField] private float maxMoveSpeed = 1.2f;
    [SerializeField] private float minMoveSpeed = 2.6f;

    [SerializeField] private float minX = 2.6f;

    FinishLine finishScript;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(20 + Random.Range(1f, 20), transform.position.y, transform.position.z);
        transform.position += Vector3.forward * Random.Range(2.4f, 6f);
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        transform.localScale += new Vector3(Random.Range(minRandomScaling, 0.2f), Random.Range(minRandomScaling, 0.2f), 1f);

        finishScript = GameObject.FindObjectOfType<FinishLine>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed * (1.0f + (Time.timeSinceLevelLoad * 0.05f));

        if (transform.position.x < -16f && finishScript.bCanRespawn)
        {
            transform.position = new Vector3(20 + Random.Range(1f, 20), transform.position.y, transform.position.z);
            transform.localScale += new Vector3(Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), 1f);
        }
    }
}
