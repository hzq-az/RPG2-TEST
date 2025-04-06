using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpellCastState : EnemyState
{
    private Enemy_Boss enemy;

    private int amountOfSpells;
    
    private float spellTimer;
    public BossSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();

        amountOfSpells = enemy.amountOfSpells;
        spellTimer =  0.5f;

    }
    public override void Update()
    {
        base.Update();
        spellTimer -= Time.deltaTime;

        if (CanCast())
        {
            enemy.CastSpell();
        }

        if(amountOfSpells <=0)
            stateMachine.ChangeState(enemy.teleportState);
    }
    public override void Exit()
    {
        base.Exit();
        enemy.LastTimeCast = Time.time;
    }   
    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }
        return false;
    }
}

