using UnityEngine;
using UnityEngine.UI;

public class ResetSkill : MonoBehaviour
{
    private GameObject[] skillTag;
    private GameObject skillManager;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ResetAllSkills);
        skillTag = GameObject.FindGameObjectsWithTag("Skill");
        skillManager = GameObject.FindGameObjectWithTag("SkillManager");
    }


    private void ResetAllSkills()
    {
        ChangSkillsColor();
        ResetSwordSkills();
        ResetBlackHole();
        ResetCloneSkill();
        ResetCrystalSkill();
        ResetDashSkill();
        ResetDodgeSkill();
        ResetParrySkill();
    }

    private void ChangSkillsColor()
    {

        //foreach(GameObject st in skillTag)  
        //{
        //    st.GetComponent<Image>().color = new Color(137,94,94,255);
        //}
        for (int i = 0; i < skillTag.Length; i++)
        {
            //if (skillTag[i].GetComponent<Image>().color == new Color(255, 255, 255, 255))
            //{
            //    Debug.Log("change");
            //}
            //    skillTag[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            skillTag[i].GetComponent<Image>().color = new Color(137 / 255f, 94 / 255f, 94 / 255f, 255 / 255f);
        }
    }

    private void ResetSwordSkills()
    {
        skillManager.GetComponent<Sword_Skill>().bounceUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().pierceUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().spinUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().swordUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().timeStopUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().vulnerableUnlockButton.unlocked = false;
        skillManager.GetComponent<Sword_Skill>().timeStopUnlocked = false;
        skillManager.GetComponent<Sword_Skill>().vulnerableUnlocked = false;
    }
    private void ResetBlackHole()
    {
        skillManager.GetComponent<Blackhole_Skill>().blackHoleUnlockButton.unlocked = false;
        skillManager.GetComponent<Blackhole_Skill>().blackHoleUnlocked = false;
    }
    private void ResetCloneSkill()
    {
        skillManager.GetComponent<Clone_Skill>().cloneAttackUnlockButton.unlocked = false;
        skillManager.GetComponent<Clone_Skill>().canAttack = false;
        skillManager.GetComponent<Clone_Skill>().aggresiveCloneUnlockButton.unlocked = false;
        skillManager.GetComponent<Clone_Skill>().canApplyOnHitEffect = false;
        skillManager.GetComponent<Clone_Skill>().multipleUnlockButton.unlocked = false;
        skillManager.GetComponent<Clone_Skill>().canDuplicateClone = false;
        skillManager.GetComponent<Clone_Skill>().crystalInsteadUnlockButton.unlocked = false;
        skillManager.GetComponent<Clone_Skill>().crystalInsteadOfClone = false;

    }
    private void ResetCrystalSkill()
    {
        skillManager.GetComponent<Crystal_Skill>().unlockCloneInsteadButton.unlocked = false;
        skillManager.GetComponent<Crystal_Skill>().cloneInsteadOfCrystal = false;
        skillManager.GetComponent<Crystal_Skill>().unlockCrystalButton.unlocked = false;
        skillManager.GetComponent<Crystal_Skill>().crystalUnlocked = false;
        skillManager.GetComponent<Crystal_Skill>().unlockExplosiveButton.unlocked = false;
        skillManager.GetComponent<Crystal_Skill>().canExplode = false;
        skillManager.GetComponent<Crystal_Skill>().unlockMovingCrystalButton.unlocked = false;
        skillManager.GetComponent<Crystal_Skill>().canMoveToEnemy = false;
        skillManager.GetComponent<Crystal_Skill>().unlockMultiStackButton.unlocked = false;
        skillManager.GetComponent<Crystal_Skill>().canUseMultiStacks = false;
    }
    private void ResetDashSkill()
    {
        skillManager.GetComponent<Dash_Skill>().dashUnlockeButton.unlocked = false;
        skillManager.GetComponent<Dash_Skill>().dashUnlocked = false;
        skillManager.GetComponent<Dash_Skill>().cloneOnDashUnlockedButton.unlocked = false;
        skillManager.GetComponent<Dash_Skill>().cloneOnDashUnlocked = false;
        skillManager.GetComponent<Dash_Skill>().cloneOnArrivalsUnlockedButton.unlocked = false;
        skillManager.GetComponent<Dash_Skill>().cloneOnArrivalsUnlocked = false;
    }
    private void ResetDodgeSkill()
    {
        skillManager.GetComponent<Dodge_Skill>().unlockDodgeButton.unlocked = false;
        skillManager.GetComponent<Dodge_Skill>().dodgeUnlocked = false;
        skillManager.GetComponent<Dodge_Skill>().unlockMirageDodge.unlocked = false;
        skillManager.GetComponent<Dodge_Skill>().dodgeMirageUnlocked = false;
    }
    private void ResetParrySkill()
    {
        skillManager.GetComponent<Parry_Skill>().parryUnlockedButton.unlocked = false;
        skillManager.GetComponent<Parry_Skill>().parryUnlocked = false;
        skillManager.GetComponent<Parry_Skill>().restoreUnlockedButton.unlocked = false;
        skillManager.GetComponent<Parry_Skill>().restoreUnlocked = false;
        skillManager.GetComponent<Parry_Skill>().parryWithMirageUnlockedButton.unlocked = false;
        skillManager.GetComponent<Parry_Skill>().parryWithMirageUnlocked = false;
    }
}
