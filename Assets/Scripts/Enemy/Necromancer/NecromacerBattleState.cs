using System.Collections;
using UnityEngine;

public class NecromacerBattleState : EnemyState
{
    private Necromancer enemy;
  //  private int moveDir;
  //  private Transform player;
    private int timer = 4;
    public NecromacerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
       // player = PlayerManager.instance.player.transform;
        //if (player.GetComponent<PlayerStats>().isDead)
        //    stateMachine.ChangeState(enemy.moveState);


    }
    public override void Update()
    {
        base.Update();

        stateTimer = enemy.battleTime;
        timer--;

        //if (player.position.x > enemy.transform.position.x)
        //    moveDir = 1;
        //else if (player.position.x < enemy.transform.position.x)
        //    moveDir = -1;

        if(timer < 0 ) 
           stateMachine.ChangeState(enemy.idleState);
    }
    public override void Exit()
    {
        base.Exit();
        if (!Cansummon())
        {
            enemy.CanCall();
            Debug.Log("called");
        }

    }
    public bool Cansummon()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

}
