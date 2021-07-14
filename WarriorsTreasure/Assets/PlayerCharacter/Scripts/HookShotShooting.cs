using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class HookShotShooting : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidBody;

    private Vector2 direction;
    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = direction * speed;
    }
    //Update is called once per frame
    void Update()
    {

    }

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()// destroys the game object once it is no longer seen a temperary destroy for now
    {
        Destroy(gameObject);
    }
}
