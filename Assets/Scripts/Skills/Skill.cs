using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    public float cooldownTimer;

    protected Player player;
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        CheckUnlock();
    }


    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    protected virtual void CheckUnlock()
    {

    }
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        // Debug.Log("cooldown");
        player.fx.CreatePoPupUpText("CoolDown");
        return false;
    }

    public virtual void UseSkill()
    {

    }
    protected virtual Transform FindCloestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        float closetDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }

            }
        }
        return closestEnemy;
    }
}
