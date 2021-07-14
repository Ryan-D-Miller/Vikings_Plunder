using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour {

	  //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().Attack = true;

        animator.SetFloat("speed", 0);

        if (animator.tag == "Player")
        {
            if (Player.Instance.OnGround)// if the player is on the ground and attacks he will stop moving
            {
                Player.Instance.MyRigidBody.velocity = Vector2.zero;
            }
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<Character>().Attack = false;// on exit of animation sets attack to false
        if (stateInfo.IsTag("Attack"))
        {
			animator.GetComponent<Character>().StartCoroutine(animator.GetComponent<Character>().MeleeAttack());
        }
        animator.ResetTrigger("attack");
        animator.ResetTrigger("throw");// resets the attack trigger
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
