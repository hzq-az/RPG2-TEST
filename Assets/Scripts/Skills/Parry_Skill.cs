using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    public UI_SkillTreeSlot parryUnlockedButton;
    public bool parryUnlocked;

    [Header("Parry restore")]
    public UI_SkillTreeSlot restoreUnlockedButton;
    public bool restoreUnlocked;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPercentage;

    [Header("Parry with mirage")]
    public UI_SkillTreeSlot parryWithMirageUnlockedButton;
    public bool parryWithMirageUnlocked;
    public override void UseSkill()
    {
        base.UseSkill();
        if(restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPercentage);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }
    protected override void Start()
    {
        base.Start();

        parryUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }
    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();

    }
    private void UnlockParry()
    {
        if (parryUnlockedButton.unlocked)
            parryUnlocked = true;
    }
    private void UnlockParryRestore()
    {
        if(restoreUnlockedButton.unlocked)
            restoreUnlocked = true;
    }
    private void UnlockParryWithMirage()
    {
        if(parryWithMirageUnlockedButton.unlocked)
            parryWithMirageUnlocked = true;
    }
    public void MakeNirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }
}
