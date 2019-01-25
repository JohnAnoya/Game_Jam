using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Character : MonoBehaviour
{

    // Makes a private reference to Rigidbody2D Component
    Rigidbody2D rb;

    // Makes a public reference to Rigidbody2D Component
    // - Shown in Inspector
    public Rigidbody2D rb2;

    // Variable to control speed of GameObject
    public float speed;

    // Variable to control jumpForce of GameObject
    public float jumpForce;

    // Variables to tell if Character should jump or not
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    // Makes a private reference to Animator Component
    Animator anim;

    // Might be used later on as an add on
    SuperPower sp;

    // Use this for initialization
    void Start()
    {

        // Assigning 'tags' and 'name' through script
        tag = "Player";
        name = "Player";

        // Used to get and save a reference to the Rigidbody2D Component
        rb = GetComponent<Rigidbody2D>();

        // Change variables of Rigidbody2D after saving a reference
        rb.mass = 1.0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // Check if speed variable was set in the inspector
        if (speed <= 0 || speed > 5.0f)
        {
            // Assign a default value if one was not set
            speed = 5.0f;

            // Prints a warning message to the Console
            // - Open Console by going to Window-->Console (or Ctrl+Shift+C)
            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }

        // Check if jumpForce variable was set in the inspector
        if (jumpForce <= 0 || jumpForce > 10.0f)
        {
            jumpForce = 10.0f;
            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + jumpForce);
        }

        // Check if groundCheckRadius variable was set in the inspector
        if (groundCheckRadius <= 0)
        {
            // Assign a default value if one was not set
            groundCheckRadius = 0.1f;

            // Prints a warning message to the Console
            // - Open Console by going to Window-->Console (or Ctrl+Shift+C)
            Debug.LogWarning("Ground Check Radius not set. Defaulting to " + groundCheckRadius);
        }

        // Check if groundCheck variable was set in the inspector
        if (!groundCheck)
        {
            // Prints a warning message to the Console
            // - Open Console by going to Window-->Console (or Ctrl+Shift+C)
            Debug.LogError("Ground Check not set in Inspector.");

            // Find gameObject to attach
            groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        }

        // Used to get and save a reference to the Animator Component
        anim = GetComponent<Animator>();

        // Check if anim variable was set in the inspector
        if (!anim)
        {
            // Prints a warning message to the Console
            // - Open Console by going to Window-->Console (or Ctrl+Shift+C)
            Debug.LogError("Animator not found on " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if left or right keys are pressed
        // - Gives decimals from -1 to +1
        //float moveValue = Input.GetAxis("Horizontal");

        // - Gives -1, 0, +1
        float moveValue = Input.GetAxisRaw("Horizontal");

        // Check if Character is touching anything labeled as Ground/Platform/Jumpable
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, isGroundLayer);

        // Check if Character is grounded
        if (isGrounded)
        {
            // Check if Jump was pressed (aka Space)
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");

                //rb.AddForce(new Vector2(0, 10.0f), ForceMode2D.Impulse);

                // Vector2.up --> new Vector2(0,1)
                // Vector2.down --> new Vector2(0,-1)
                // Vector2.left --> new Vector2(-1,0)
                // Vector2.right --> new Vector2(1,0)
                // Vector2.zero --> new Vector2(0,0)
                // Vector2.one --> new Vector2(1,1)

                // Applies a force in UP direction
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        // Check if Space was pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        // Make sure Rigidbody2D is attached before doing stuff
        if (rb)
            // Make player move left or right based off moveValue
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        // Make sure Animator is attached before doing stuff
        if (anim)
        {
            // Activate tranisitions in Animator
            anim.SetBool("Grounded", isGrounded);
            anim.SetFloat("Movement", Mathf.Abs(moveValue));
        }
    }
}
