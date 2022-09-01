using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] Vector2 jumpVelocity = Vector2.up;
    [SerializeField] public  Sprite[] spriteArray;
    [SerializeField] public Sprite[] spriteArrayCape;
    [SerializeField] public bool bInvincible = false;

    private BoxCollider2D collider2D;
    private Rigidbody2D rigidbody2D;
    private Color cOriginalColor;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererCape;
    private int spriteIndex;
    private InputAction movement;
    private PlayerActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        bInvincible = false;
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererCape = GameObject.Find("PupCape").GetComponent<SpriteRenderer>();
        cOriginalColor = spriteRenderer.color;
        spriteRenderer.sprite = spriteArray[0];
        spriteIndex = 0;

        AudioListener.pause = false;
        Time.timeScale = 1;

        //just call the function to animate the cape over and over... cheesy but it works.
        InvokeRepeating("AnimateCape", 0.15f, 0.15f);
    }

    private void Awake()
    {
        playerInputActions = new PlayerActions();
    }

    private void OnEnable()
    {
        movement = playerInputActions.PlayerControls.Flap;
        movement.Enable();

        playerInputActions.PlayerControls.Flap.performed += DoFlap;
        playerInputActions.PlayerControls.Flap.Enable();
    }

    private void DoFlap(InputAction.CallbackContext obj)
    {
        Debug.Log("Flap");
        rigidbody2D.velocity = jumpVelocity * 1.5f;
        ChangeSprite();
    }

    private void OnDisable()
    {
        movement.Disable();
        playerInputActions.PlayerControls.Flap.Disable();
    }

    void randomizeColor()
    {
        SetSpriteColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 255f));
    }

    void SetSpriteColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    void ChangeSprite()
    {
        if (spriteIndex == 0)
        {
            spriteRenderer.sprite = spriteArray[1];
            spriteIndex = 1;
        }
        else
        {
            spriteIndex = 0;
            spriteRenderer.sprite = spriteArray[0];
        }
    }

    void AnimateCape()
    {
        if (spriteIndex == 0)
        {
            spriteRendererCape.sprite = spriteArrayCape[1];
            spriteIndex = 1;
        }
        else
        {
            spriteIndex = 0;
            spriteRendererCape.sprite = spriteArrayCape[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * OLD
         * if (Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.velocity = jumpVelocity * 1.5f;
            ChangeSprite();
        }
        */
        //if (transform.rotation.z > 0.001f || transform.rotation.z < -0.001f)
        //always slowly rotate back to the 0 z rotation so we arent in a goofy rotation to due to collisions and forces rotating the rigidbody
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1.0f * Time.deltaTime);

        if (bInvincible)
        {
            randomizeColor();

            if (collider2D.enabled)
                collider2D.enabled = false;

            if (transform.position.y > 4f)
                transform.position = new Vector3(transform.position.x, 4f);

            if (transform.position.y < -3.45f)
                transform.position = new Vector3(transform.position.x, -3.45f);
        }
        else
        {
            collider2D.enabled = true;
            SetSpriteColor(cOriginalColor);

            //resets the player position if we manage to get out of bounds somehow
            if (transform.position.y > 4.5f)
                transform.position = new Vector3(transform.position.x, 0f);
            else if (transform.position.y < -4.0f)
                transform.position = new Vector3(transform.position.x, 0f);
        }
    }
}
