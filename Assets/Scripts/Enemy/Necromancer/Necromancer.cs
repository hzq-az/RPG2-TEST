using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Necromancer : Enemy
{
    [SerializeField] private GameObject enemyPrefab;
    public NecromacerIdleState idleState { get; private set; }
    public NecromacerBattleState battleState { get; private set; }
    public NecromancerDeadState deadState { get; private set; }
    public NecromancerMoveState moveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        //SetupDefallFacingDir(-1);


        idleState = new NecromacerIdleState(this, stateMachine, "Idle", this);
        battleState = new NecromacerBattleState(this, stateMachine, "Battle", this);
        moveState = new NecromancerMoveState(this, stateMachine, "Move", this);
        deadState = new NecromancerDeadState(this, stateMachine, "Dead", this);

    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public void CanCall()
    {
        Vector3 enemyposition1 = new Vector3(transform.position.x - 3, transform.position.y);
        Vector3 enemyposition2 = new Vector3(transform.position.x + 3, transform.position.y);
        Instantiate(enemyPrefab, enemyposition1, Quaternion.identity);
        Instantiate(enemyPrefab, enemyposition2, Quaternion.identity);

    }
}
