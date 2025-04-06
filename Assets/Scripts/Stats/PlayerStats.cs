using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    public override void Die()
    {
        base.Die();

        player.Die();
        GameManager.instance.lostCurrencyAmount = PlayerManager.instance.currency;
        PlayerManager.instance.currency = 0;
        GetComponent<PlayerItemDrop>()?.GenenrateDrop();
    }
    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);
        if (currentArmor != null)
        {
            currentArmor.Effect(player.transform);
        }
    }
    public override void OnEvasion()
    {
        //Debug.Log("player avoided atack");
        player.skill.dodge.CreateMirageOnDodge();
    }
    public void CloneDamage(CharacterStats _targetStats,float _multiplier)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (_multiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            // Debug.Log("Total crit damage is" + totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
        DoMagicalDamage(_targetStats);//ÆÕ¹¥´øÄ§·¨ÉËº¦£»
    }
}
