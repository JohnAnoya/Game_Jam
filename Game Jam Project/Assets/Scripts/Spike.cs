using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spike : MonoBehaviour {

    public GameObject Spikes;

    SpriteRenderer sprite;

    bool hasTriggered;

	// Use this for initialization
	void Start ()
    {
        hasTriggered = false;

        Spikes = GetComponent<GameObject>();

        sprite = GetComponent<SpriteRenderer>();

        sprite.enabled = !sprite.enabled;
    }
	
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            sprite.enabled = !sprite.enabled;
            hasTriggered = true;
        }
        if (hasTriggered)
        {
           // sprite.enabled = !sprite.enabled;
        }

    }
}
