using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimation : MonoBehaviour {
	private Animator myAnimator;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
		if (Player.Instance.facingRight) 
		{
			myAnimator.SetTrigger ("throwRight");
		} else 
		{
			myAnimator.SetTrigger("throwLeft");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
