using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;
    
    private float idleDuration = Random.Range(3f, 8f);

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;// make sure that the enemy that is being controlled by the script is this enemy
    }

    public void Execute()
    {
        Idle();
        if(enemy.Target != null)
        {
            
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "ThrownWeapon")
        {
            enemy.Target = Player.Instance.gameObject;
        }
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)// after being in the idle state for a set amount of time it will change the state back to the partrol State
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
