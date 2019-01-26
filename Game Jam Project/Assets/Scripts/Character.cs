using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Character : MonoBehaviour
{

    Rigidbody2D rb2d;

    public float speed;
    public float jumpF;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRad;
    public LayerMask isGroundLayer;
    

    public int points { get; set; }

    // Use this for initialization
    void Start()
    {
        tag = "Player";

        points = 0;

        speed = 6.0f;
        jumpF = 9.0f;

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

        if (groundCheck)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRad, isGroundLayer);
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb2d.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            }
        }
        else
        {

        }

        rb2d.velocity = new Vector2(moveValue * speed, rb2d.velocity.y);

      
    }

}
