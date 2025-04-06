using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadious);

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(_target);


                //hit.GetComponent<Player>().Damage();
            }
        }
    }
    void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
