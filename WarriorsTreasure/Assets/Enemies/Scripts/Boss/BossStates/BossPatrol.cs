using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : IBossState
{
    private Boss boss;

    private float patrolTimer;
    private float patrolDuration = Random.Range(8f, 12f);
    public void Enter(Boss boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        if (boss.AttackTimer >= boss.AttackDuration)
        {
            boss.ChangeState(new BossAttack());
        }
        else
        {
            Patrol();

            boss.Move();
        }
    }
    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Patrol()
    {
        
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)// after being in the idle state for a set amount of time it will change the state back to the partrol State
        {
            boss.ChangeState(new BossIdle());
        }
    }

}
