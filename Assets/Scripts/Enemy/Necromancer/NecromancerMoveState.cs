using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMoveState : EnemyState
{
    private Necromancer enemy;
    private Transform player;
    private int moveDir =1;

    public NecromancerMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 2)
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 5 && enemy.battleState.Cansummon())
        { 
            stateMachine.ChangeState(enemy.battleState);
            
        }
    }
}
