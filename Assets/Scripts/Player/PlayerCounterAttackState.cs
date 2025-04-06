using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreatClone;
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreatClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadious);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 7;
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    player.skill.parry.UseSkill(); //going to restore health on parry

                    if (canCreatClone)
                    {
                        canCreatClone = false;
                        player.skill.parry.MakeNirageOnParry(hit.transform);
                    }
                }
            }
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
