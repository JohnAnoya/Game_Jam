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

        speed = 6.0f;
        jumpF = 7.5f;


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
    }
    // Update is called once per frame
    void Update()
    {
        float moveValue = Input.GetAxisRaw("Horizontal");

        Debug.Log(facingRight);

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
        else
        {

        }

        rb2d.velocity = new Vector2(moveValue * speed, rb2d.velocity.y);

        // checks if the user is not on the ground and touching a wall 
        if (!isGrounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayer);

            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (wallCheck)
                {
                    WallSliding();
                }
            }

        }

        if (wallCheck == false || isGrounded)
        {
            wallSlide = false;
        }

    }

    // this function will decrease the y velocity of the player to simulate the effect of wall sliding 
    void WallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.4f);

        wallSlide = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-80, 80) * jumpF);
            }
            else
            {
                rb2d.AddForce(new Vector2(80, 80) * jumpF);
            }
        }

    }


}
