using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;
    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreatHotKeys = true;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private int amountOfAttacks = 4;
    private float cloneAttackCoolDown = 0.3f;
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKey = new List<GameObject>();

    public bool playerCanExitState { get; private set; }

    public void SetUpBlackHole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCoolDown, float _blackholeDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCoolDown = _cloneAttackCoolDown;

        blackholeTimer = _blackholeDuration;

        if (SkillManager.instance.clone.crystalInsteadOfClone)
            playerCanDisapear = false;

    }
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if (blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;
            if (targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackHoleAbility();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }
        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;

        DestoryHotKeys();
        cloneAttackReleased = true;
        canCreatHotKeys = false;
        if (playerCanDisapear)
        {
            playerCanDisapear = false;
            PlayerManager.instance.player.fx.MakeTransprent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCoolDown;
            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;
            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;

            if (SkillManager.instance.clone.crystalInsteadOfClone)
            {
                SkillManager.instance.crystal.CreateCrystal();
                SkillManager.instance.crystal.CurrentCrydtalChooseRandomTarget();
            }
            else
            {
                SkillManager.instance.clone.CreatClone(targets[randomIndex], new Vector3(xOffset, 0));
            }

            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackHoleAbility", 0.5f);
            }
        }
    }

    private void FinishBlackHoleAbility()
    {
        DestoryHotKeys();
        playerCanExitState = true;
        // PlayerManager.instance.player.ExistBlackHoleAbility();
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void DestoryHotKeys()
    {
        if (createdHotKey.Count <= 0)
            return;

        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FrezzeTime(true);

            CreratHotKey(collision);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FrezzeTime(false);
        }
    }
    //private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FrezzeTime(false);

    private void CreratHotKey(Collider2D collision)
    {
        if (keyCodeList.Count < 0)
        {
            Debug.LogWarning("not enough hot keys in list");
            return;
        }
        if (!canCreatHotKeys)
            return;

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKey.Add(newHotKey);

        KeyCode chooseKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(chooseKey);

        Blackhole_HotKey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKey_Controller>();

        newHotKeyScript.SetupHotKey(chooseKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
