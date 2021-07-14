using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorzMovingPlatform : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float travelDuration;
    public float travelCooldown = 3f;
    public bool travelRight = true;
    public float travelSpeed;

	// Use this for initialization
	void Start ()
    {
        //myRigidBody = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        travelDuration += Time.deltaTime;
        if(travelDuration >= travelCooldown)
        {
            travelRight = !travelRight;
            travelDuration = 0;
        }
        if(travelRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * travelSpeed);
            //myRigidBody.velocity = new Vector2(Time.deltaTime * travelSpeed, myRigidBody.velocity.y);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * travelSpeed);
            //myRigidBody.velocity = new Vector2(Time.deltaTime * -travelSpeed, myRigidBody.velocity.y);
        }
	}
}
