using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Crystal mirage")]
    public UI_SkillTreeSlot unlockCloneInsteadButton;
    public bool cloneInsteadOfCrystal;

    [Header("Crystal simple")]
    public UI_SkillTreeSlot unlockCrystalButton;
    public bool crystalUnlocked;

    [Header("Explosive crystal")]
    public UI_SkillTreeSlot unlockExplosiveButton;
    public bool canExplode;

    [Header("Moving crystal")]
    public UI_SkillTreeSlot unlockMovingCrystalButton;
    public bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    [Header("Multi stacking crystal ")]
    public UI_SkillTreeSlot unlockMultiStackButton;
    public bool canUseMultiStacks;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCoolDown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockCloneInsteadButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMirage);
        unlockExplosiveButton.GetComponent<Button>().onClick.AddListener(UnlockExplosionCrystal);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiStackButton.GetComponent<Button>().onClick.AddListener(UnlockMutiStack);
    }
    #region Ulock crystal skill

    protected override void CheckUnlock()
    {
        UnlockCrystal();
        UnlockCrystalMirage();
        UnlockExplosionCrystal();
        UnlockMovingCrystal();
        UnlockMutiStack();
       // Debug.Log("444");
    }
    private void UnlockCrystal()
    {
        if (unlockCrystalButton.unlocked)
        {
            crystalUnlocked = true;
           // Debug.Log("777");
        }
    }
    private void UnlockCrystalMirage()
    {
        if (unlockCloneInsteadButton.unlocked)
            cloneInsteadOfCrystal = true;
    }
    private void UnlockExplosionCrystal()
    {
        if(unlockExplosiveButton.unlocked)
            canExplode = true;
    }
    private void UnlockMovingCrystal()
    {
        if(unlockMovingCrystalButton.unlocked)
            canMoveToEnemy = true;
    }
    private void UnlockMutiStack()
    {
        if(unlockMultiStackButton.unlocked)
            canUseMultiStacks = true;
    }
#endregion
    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (canMoveToEnemy)
                return;


            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (cloneInsteadOfCrystal)
            {
                SkillManager.instance.clone.CreatClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
            }
        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCrystalScrit = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCrystalScrit.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindCloestEnemy(currentCrystal.transform), player);
      
    }
    public void CurrentCrydtalChooseRandomTarget() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacks)
        {
            if (crystalLeft.Count > 0)
            {
                if (crystalLeft.Count == amountOfStacks)
                {
                    Invoke("RestAbility", useTimeWindow);
                }

                cooldown = 0;
                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalLeft.Remove(crystalToSpawn);
                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindCloestEnemy(newCrystal.transform), player);

                if (crystalLeft.Count <= 0)
                {
                    // Debug.Log("//coolsown the skill");
                    cooldown = multiStackCoolDown;
                    RefilCryStal();
                }
                return true;
            }

        }
        return false;
    }
    private void RefilCryStal()
    {
        int amountToAdd = amountOfStacks - crystalLeft.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }
    private void RestAbility()
    {
        if (cooldownTimer > 0)
            return;
        cooldownTimer = multiStackCoolDown;
        RefilCryStal();
    }
}
