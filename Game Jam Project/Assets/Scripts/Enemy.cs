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
        
        // always move foward
        Vector2 myVel = rb.velocity;
        myVel.x = speed;
        rb.velocity = myVel;
	}
}
