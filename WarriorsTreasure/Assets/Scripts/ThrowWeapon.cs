using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    public float speed;
    private Vector2 direction;
    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       myRigidBody.velocity = direction * speed;
    }

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MeleeAxe")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
