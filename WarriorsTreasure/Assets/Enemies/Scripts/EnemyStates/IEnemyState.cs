using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{// an interface for a basic enemy within the game
    void Execute();
    void Enter(Enemy enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);

}
