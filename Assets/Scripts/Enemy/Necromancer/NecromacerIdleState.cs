using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromacerIdleState : EnemyState
{
    private Necromancer enemy;
   

    public NecromacerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy =_enemy;
    }
    public override void Enter()
    {
        base.Enter();

        
        stateTimer = enemy.idelTime;

    }
    public override void Update()
    {
        base.Update();

       
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
