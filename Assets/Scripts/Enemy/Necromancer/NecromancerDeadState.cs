using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerDeadState : EnemyState
{
    private Necromancer enemy;

    public NecromancerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
   
}
