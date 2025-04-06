using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat soulsDropAmount;

    [Header("Level detail")]
    public int level = 1;

    [Range(0f, 1f)]
    public float percentageModifier = 0.4f;

    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(100);
        ApplyLevelModifiers();

        base.Start();
        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();

    }

    private void ApplyLevelModifiers()
    {
        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(strength);
        Modify(agility);
        Modify(vitality);
        Modify(intelligence);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(lightingDamage);
        Modify(iceDamage);
        Modify(soulsDropAmount);
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifer = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        
    }
    public override void Die()
    {
        base.Die();
        enemy.Die();
        myDropSystem.GenenrateDrop();
        PlayerManager.instance.currency += soulsDropAmount.GetValue();
        Destroy(gameObject, 3f);
    }
}
