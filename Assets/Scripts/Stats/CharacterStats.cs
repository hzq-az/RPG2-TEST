using System.Collections;
using UnityEngine;


public enum StatType
{
    strength,
    agility,
    intelegence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicRes,
    fireDamage,
    iceDamage,
    lightingDamage
}
public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major stats")]
    public Stat strength;           // 1 point increase damage by 1 and crit.power by 1%
    public Stat agility;            // 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence;       // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat vitality;           // 1 point incredase health by 5 points

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;


    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;          // default value 150%

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;


    public bool isIgnited;          // does damage over time
    public bool isChilled;          // reduce armor by 20%
    public bool isShocked;          // reduce accuracy by 20%

    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    [SerializeField] private float ailmentsDuration = 4;

    private float igniteDamageCoolDown = 0.3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;

    public int currentHealth;
    public System.Action onHealthChanged;
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }
    public bool isVulnerable { get; private set; }
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();
    }

    protected void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;
        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if (isIgnited)
            ApplyIgniteDamage();
    }
    public void MakeVulnerableFor(float _duration)
    {
        StartCoroutine(VulnerableForCorutine(_duration));
    }
    private IEnumerator VulnerableForCorutine(float _duration)
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_duration);

        isVulnerable = false;
    }
    public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModify)
    {
        StartCoroutine(StatModCorotine(_modifier, _duration, _statToModify));
    }
    private IEnumerator StatModCorotine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);
        yield return new WaitForSeconds(_duration);
        _statToModify.RemoveModifier(_modifier);
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        bool criticalstrike = false;

        if (TargetCanAvoidAttack(_targetStats))
            return;

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue() + strength.GetValue();


        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            // Debug.Log("Total crit damage is" + totalDamage);
            criticalstrike = true;
        }

        fx.CreateHitFx(_targetStats.transform, criticalstrike);

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
        DoMagicalDamage(_targetStats);//�չ���ħ���˺���
    }
    #region Magical and damage aliments
    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();
        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();
        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;
        AtttempToApplyAliments(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }

    private void AtttempToApplyAliments(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _iceDamage && _lightingDamage > _fireDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < 0.3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                // Debug.Log("Applied fire");
                return;
            }
            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                // Debug.Log("Applied ice");

                return;
            }
            if (Random.value < 0.5f && _lightingDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                // Debug.Log("Applied lighting");
                return;
            }
        }
        if (canApplyIgnite)
        {
            _targetStats.SetUpIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.2f));
        }

        if (canApplyShock)
            _targetStats.SetUpShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * 0.1f));

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;

        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFxFor(ailmentsDuration);
        }
        if (_chill && canApplyChill)
        {
            chilledTimer = ailmentsDuration;
            isChilled = _chill;

            float slowPercentage = 0.3f;
            GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);

            fx.ChillFxFor(ailmentsDuration);
        }
        if (_shock && canApplyShock)
        {

            if (!isShocked)
            {
                ApplyShock(_shock);
            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;
                //�ҵ������Ŀ����ˣ�
                HitNearestTargetWithShockStrike();
            }
        }
    }

    public void ApplyShock(bool _shock)
    {
        if (isShocked)
            return;

        isShocked = _shock;
        shockedTimer = ailmentsDuration;

        fx.ShockeFxFor(ailmentsDuration);
    }

    private void HitNearestTargetWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closetDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
            if (closestEnemy == null)
                closestEnemy = transform;
        }

        if (closestEnemy != null)
        {
            GameObject newShockeStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

            newShockeStrike.GetComponent<ShockStrike_Controller>().SetUp(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }
    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0)
        {
            // Debug.Log("TAKE TURN DAMAGE" + igniteDamage);

            DecreaseHealthBy(igniteDamage);
            if (currentHealth <= 0 && !isDead)
                Die();


            igniteDamageTimer = igniteDamageCoolDown;
        }
    }


    public void SetUpIgniteDamage(int _damage) => igniteDamage = _damage;
    public void SetUpShockStrikeDamage(int _damage) => shockDamage = _damage;
    #endregion
    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);
        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        // Debug.Log(_damage);
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;
        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        if (isVulnerable)
            _damage = Mathf.RoundToInt(_damage * 1.1f);

        currentHealth -= _damage;
        if(_damage >0)
            fx.CreatePoPupUpText(_damage.ToString());

        if (onHealthChanged != null)
            onHealthChanged();
    }

    public virtual void Die()
    {
        isDead = true;
    }
    public void KillEntity()
    {
        if (!isDead)
            Die();
    }

    public void MakeInvincible(bool _invicible) => isInvincible = _invicible;
    #region Stat calculation
    protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * 0.8f);
        else
            totalDamage -= _targetStats.armor.GetValue();

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public virtual void OnEvasion()
    {

    }
    protected bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            _targetStats.OnEvasion();
            return true;
        }
        return false;
    }

    protected bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();
        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    protected int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;
        // Debug.Log("crit power" + totalCritPower);

        float critDamage = _damage * totalCritPower;
        // Debug.Log("round up" +  critDamage);    

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
    #endregion
    public Stat GetStat(StatType _statType)
    {
        if (_statType == StatType.strength) return strength;
        else if (_statType == StatType.agility) return agility;
        else if (_statType == StatType.intelegence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChance) return critChance;
        else if (_statType == StatType.critPower) return critPower;
        else if (_statType == StatType.health) return maxHealth;
        else if (_statType == StatType.armor) return armor;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.magicRes) return magicResistance;
        else if (_statType == StatType.fireDamage) return fireDamage;
        else if (_statType == StatType.iceDamage) return iceDamage;
        else if (_statType == StatType.lightingDamage) return lightingDamage;

        return null;
    }
}
