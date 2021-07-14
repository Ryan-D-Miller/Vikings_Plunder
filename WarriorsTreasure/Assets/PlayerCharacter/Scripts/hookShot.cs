using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookShot : MonoBehaviour {
    DistanceJoint2D joint;
    RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;
	// Use this for initialization
	void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 1), distance,mask);
            Debug.DrawRay(transform.position, new Vector2(transform.localScale.x, 1));
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Debug.Log("We thru the ray cast");
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);
            }
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
        }
	}
}
