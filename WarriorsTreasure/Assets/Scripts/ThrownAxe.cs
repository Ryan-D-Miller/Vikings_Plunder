using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ThrownAxe : MonoBehaviour
{
    public float speed;
    GameObject playerGO;
    private Rigidbody2D myRigidBody;

    private Vector2 direction;
    private Animator myAnimator;
	public LayerMask hookTargets;
	public float hookDistance = 10f;
    public bool HitGround { get; set; }
	public float throwTime = 20f;
    public float slightPullWhenHooked;
    Rigidbody2D PlayerRB;
    DistanceJoint2D joint;
   public LineRenderer line;
    public float climbSpeed;
    public float maxJointDistance;

    // Use this for initialization
    void Start ()
    {
        playerGO = GameObject.FindWithTag("Player");
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        PlayerRB = playerGO.GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, playerGO.transform.position);
        

}

    private void FixedUpdate()
    {
         myRigidBody.velocity = direction * speed;
        if (Player.Instance.axeHasHit == true)
        {
            float climb = Input.GetAxis("Vertical");
            
            Climbing(climb);
        }
    }
    //Update is called once per frame
    void Update ()
    {
		throwTime -= Time.deltaTime;
		if (throwTime < 0 && !Player.Instance.axeHasHit) {
			Destroy (gameObject);

		}
        if (Input.GetKeyUp(KeyCode.E))// if the e button is lifted the axe is destroyed
        {
            Destroy(gameObject);
            
            Player.Instance.axeHasHit = false;
            joint.enabled = false;
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, playerGO.transform.position);

    }

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;
    }

   /* private void OnBecameInvisible()// destroys the game object once it is no longer seen a temperary destroy for now
    {
        Destroy(gameObject);

    }*/
    private void OnTriggerEnter2D(Collider2D other) // when an axe hit the ground things happen
    {
        
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "movingPlatform")
        {
            
            speed = 0;
            myAnimator.speed = 0f;
            myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
            Player.Instance.axeHasHit = true;
            myRigidBody.isKinematic = true;
            joint.enabled = true;
            joint.connectedBody = PlayerRB;
            joint.distance = Vector2.Distance(PlayerRB.position, transform.position) - slightPullWhenHooked;
            gameObject.transform.parent = other.transform;
            
        }
        else if(other.gameObject.tag == "Enemy")//if the axe hits an enemy destroy the axe and set axe throw back to false might change a few things here later but this works for now
        {
            Destroy(gameObject);
            
        }
    }
    private void Climbing(float climb)
    {
        
        if(climb > 0)
        {
            joint.distance = joint.distance - climbSpeed;
        }
        else if (climb < 0)
        {
            if (joint.distance < maxJointDistance)
            {
                joint.distance = joint.distance + climbSpeed;
            }
        }
        
    }
}
