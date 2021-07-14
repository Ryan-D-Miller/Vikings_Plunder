using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    private float throwTimer;
    private float throwCoolDown = 3f;
    private bool canThrow = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowWeapon();
        if(enemy.Target != null)// if the enemy has a target he attacks
        {
            //enemy.Move();
        }
        else // if the lose the target it goes to goes back to idle
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

    private void ThrowWeapon()
    {
        throwTimer += Time.deltaTime;

        if(throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if(canThrow)
        {
            canThrow = false;
            enemy.EnemyAudioSource.clip = enemy.bowFire;
            enemy.EnemyAudioSource.Play();
            enemy.MyAnimator.SetTrigger("throw");
        }
    }
}
