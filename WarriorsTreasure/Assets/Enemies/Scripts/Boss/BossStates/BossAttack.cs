using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : IBossState
{
    private Boss boss;
    public void Execute()
    {
        Attack();
        boss.AttackTimer = 0;
        boss.AttackDuration = Random.Range(10f, 20f);
        boss.ChangeState(new BossIdle());
    }
    public void Enter(Boss boss)
    {
        this.boss = boss;
    }
    public void Exit()
    {

    }
    public void OnTriggerEnter(Collider2D other)
    {

    }
    private void Attack()
    {
        Debug.Log("I have attacked");
        boss.MyAnimator.SetTrigger("attack");
    }
}
