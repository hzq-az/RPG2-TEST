using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]

public class HealEffect : ItemEffect
{
    [Range(0, 1f)]
    [SerializeField] private float healPercent;
    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);

        playerStats.IncreaseHealthBy(healAmount);
    }
}
