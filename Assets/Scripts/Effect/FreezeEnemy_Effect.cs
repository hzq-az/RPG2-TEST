using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Freeze Enemies effect", menuName = "Data/Item Effect/Freeze Enemies")]
public class FreezeEnemy_Effect : ItemEffect
{
    [SerializeField] private float duration;
    public override void ExecuteEffect(Transform _transform)
    {
        PlayerStats playerstats= PlayerManager.instance.player.GetComponent<PlayerStats>();
        if (playerstats.currentHealth > playerstats.GetMaxHealthValue() * 0.1f)
            return;

        if(!Inventory.instance.CanUseArmor())
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
            
        }
    }
}
