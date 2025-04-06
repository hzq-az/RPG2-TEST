using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("Dash")]
    public UI_SkillTreeSlot dashUnlockeButton;
    public bool dashUnlocked;
    [Header("Clone on dash")]
    public UI_SkillTreeSlot cloneOnDashUnlockedButton;
    public bool cloneOnDashUnlocked;

    [Header("Clone on arrival")]
    public UI_SkillTreeSlot cloneOnArrivalsUnlockedButton;
    public bool cloneOnArrivalsUnlocked;
    public override void UseSkill()
    {
        base.UseSkill();

        // Debug.Log("left clone behind");
    }
    protected override void Start()
    {
        base.Start();

        dashUnlockeButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedCloneOnDash);
        cloneOnArrivalsUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }

    protected override void CheckUnlock()
    {
        UnlockDash();
        UnlockedCloneOnDash();
        UnlockCloneOnArrival();

    }
    private void UnlockDash()
    {
        if (dashUnlockeButton.unlocked)
            dashUnlocked = true;
    }
    private void UnlockedCloneOnDash()
    {
        if (cloneOnDashUnlockedButton.unlocked)
            cloneOnDashUnlocked = true;
    }
    private void UnlockCloneOnArrival()
    {
        if (cloneOnArrivalsUnlockedButton.unlocked)
            cloneOnArrivalsUnlocked = true;
    }
    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
            SkillManager.instance.clone.CreatClone(player.transform, Vector3.zero);

    }
    public void CloneOnArrival()
    {
        if (cloneOnArrivalsUnlocked)
            SkillManager.instance.clone.CreatClone(player.transform, Vector3.zero);

    }
}
