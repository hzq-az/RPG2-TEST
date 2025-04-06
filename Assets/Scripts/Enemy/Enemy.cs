using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]


public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    public GameObject counterImage;
    protected bool canBeStunned;


    [Header("Move info")]
    public float moveSpeed;
    public float idelTime;
    public float battleTime;
    private float defaultMoveSpeed;
    [Header("Attack info ")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }

    public EntityFX fx { get; private set; }

    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
        fx = GetComponent<EntityFX>();

    }
    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

    }

    public virtual void AssignLastAnimName(string _animBoolname) => lastAnimBoolName = _animBoolname;

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed *= (1 - _slowPercentage);
        anim.speed *= (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }
    public virtual void FrezzeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0f;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }

    }
    public virtual  void FreezeTimeFor(float _duration) => StartCoroutine(FreeezeTimerCoroutine(_duration));
    protected virtual IEnumerator FreeezeTimerCoroutine(float _seconds)
    {
        FrezzeTime(true);
        yield return new WaitForSeconds(_seconds);
        FrezzeTime(false);
    }

    #region Counter Attack Window
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    #endregion 
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
