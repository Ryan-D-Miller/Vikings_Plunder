using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : IBossState {
    private Boss boss;

    private float idleTimer;

    private float idleDuration = Random.Range(3f, 8f);
    // Use this for initialization
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
            Idle();
        }
    }
    public void Exit()
    {

    }
    // Update is called once per frame
    void Update () {
		
	}
    public void OnTriggerEnter(Collider2D other)
    {

    }
    private void Idle()
    {
        boss.MyAnimator.SetFloat("speed", 0);
        
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)// after being in the idle state for a set amount of time it will change the state back to the partrol State
        {
            boss.ChangeState(new BossPatrol());
        }
    }
}
