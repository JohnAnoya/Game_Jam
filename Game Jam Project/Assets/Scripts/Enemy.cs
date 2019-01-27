using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D rb;
    Transform tf;
    float width;
	// Use this for initialization
	void Start () {
        tf = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 lineCastPos = tf.position - tf.right * width;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - tf.right, enemyMask);


        if (!isGrounded)
        {
            Vector3 currRot = tf.eulerAngles;
            currRot.y += 180;
            tf.eulerAngles = currRot;
        }

        // always move foward
        Vector2 myVel = rb.velocity;
        myVel.x = -tf.right.x * speed;
        rb.velocity = myVel;
	}
}
