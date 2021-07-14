using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertMovingPlatform : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Transform myTransform;
    public float travelDuration;
    public float travelCooldown = 3f;
    public bool travelUp = false;
    public float travelSpeed;

    // Use this for initialization
    void Start()
    {
        //myRigidBody = GetComponent<Rigidbody2D>();
        //myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        travelDuration += Time.deltaTime;
        if (travelDuration >= travelCooldown)
        {
            travelUp = !travelUp;
            travelDuration = 0;
        }
        if (travelUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * travelSpeed); 
            //myTransform.position =  
            
            //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Time.deltaTime * travelSpeed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * travelSpeed);
            //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Time.deltaTime * -travelSpeed);
        }
    }
}
