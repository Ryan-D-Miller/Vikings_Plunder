using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy enemy;

    private float attackTimer;
    private float attackCoolDown = 3f;
    private bool canAttack = true;
    public void Enter(Enemy enemy)
    {
		this.enemy = enemy;
    }

    public void Execute()
    {
		if(enemy.Target != null && enemy.InMeleeRange)
        {
            Melee();
        }
        else if(enemy.Target != null)
        {
            enemy.MeleeMove();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
    private void Melee()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }

}
