using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private float patrolTimer;
    private float patrolDuration = Random.Range(8f, 12f);

    private Enemy enemy;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();

        enemy.Move();
        if(enemy.Target != null && enemy.InThrowRange)// if you are patrolling and you get a target the enemy will enter the ranged state
        {
            if (enemy.name == "SkeletonArcher")
            {
                enemy.ChangeState(new RangedState());
            }
 
        }
		if (enemy.Target != null) {
			
			if (enemy.name == "OrkWarrior") {
				enemy.ChangeState (new MeleeState ());
			}
		}
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "ThrownWeapon")
        {
            enemy.Target = Player.Instance.gameObject;
        }
    }
    private void Patrol()
    {
        
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)// after being in the idle state for a set amount of time it will change the state back to the partrol State
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
