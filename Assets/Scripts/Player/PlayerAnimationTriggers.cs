using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();
    void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(2, null);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadious);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                if (_target != null)
                    player.stats.DoDamage(_target);
                // hit.GetComponent<Enemy>().Damage();
                // hit.GetComponent<CharacterStats>().TakeDamge(player.stats.damage.GetValue ());

                //Inventory.instance.GetEquipment(EquipmentType.Weapon).Effect(_target.transform);
                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);
                if (weaponData != null)
                    weaponData.Effect(_target.transform);
            }
        }
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
