using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Character : MonoBehaviour
{

    Rigidbody2D rb2d;
    // Character Movement
    public float speed;
    public float jumpF;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRad;
    public LayerMask isGroundLayer;
    public bool facingRight;

    // player spawns
    public Vector2 spawnPoint;
    public bool newSpawnPoint = false;
    public bool playerDeath = false;

    // Character Walljump/Slide
    public bool wallSlide;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayer;

    public int points { get; set; }


    // Use this for initialization
    void Start()
    {
        tag = "Player";

        points = 0;

        speed = 8.0f;
        jumpF = 10.0f;


        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = 1.0f;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2d.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (!groundCheck)
        {
            groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        }

        if (groundCheckRad <= 0.0f)
        {
            groundCheckRad = 0.1f;
        }

        // setting the coordinates to the spawnpoint in scene 1.
        spawnPoint = new Vector3(rb2d.position.x, rb2d.position.y);

    }
    // Update is called once per frame
    void Update()
    {
        CharacterMovement();

    }


    void CharacterMovement()
    {
        float moveValue = Input.GetAxisRaw("Horizontal");

        if (moveValue == 1.0f)
        {
            facingRight = true;
        }
        else if (moveValue == -1.0f)
        {
            facingRight = false;
        }

        if (groundCheck)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRad, isGroundLayer);
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump") && !wallSlide)
            {
                rb2d.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            }
        }
        rb2d.velocity = new Vector2(moveValue * speed, rb2d.velocity.y);
    }
    
 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("spikes"))
        {
            playerDeath = true;

            rb2d.transform.position = spawnPoint;
        }
        playerDeath = false;
    }

}
