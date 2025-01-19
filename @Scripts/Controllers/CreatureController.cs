using System.Collections;
using UnityEngine;

public class CreatureController : BaseController
{
    public string Name { get; set; }
    public string Grade { get; set; }
    public float Speed { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public int AttackDelay { get; set; }


    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    #region State Pattern

    Define.CreatureState _creatureState;
    public virtual Define.CreatureState CreatureState
    {
        get { return _creatureState; }
        set
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    public virtual void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                animator.Play("Idle");
                break;
            case Define.CreatureState.Walk:
                animator.Play("Walk");
                break;
            case Define.CreatureState.Attack:
                PlayRandomAttackAnimation();
                break;
            case Define.CreatureState.Hurt:
                animator.Play("Hurt");
                break;
            case Define.CreatureState.Death:
                animator.Play("Death");
                break;
        }
    }

    public override void UpdateController()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                UpdateIdle();
                break;
            case Define.CreatureState.Walk:
                UpdateWalk();
                break;
            case Define.CreatureState.Attack:
                UpdateAttack();
                break;
            case Define.CreatureState.Hurt:
                UpdateAttack();
                break;
            case Define.CreatureState.Death:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateWalk() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateHurt() { }
    protected virtual void UpdateDead() { }

    public void PlayRandomAttackAnimation()
    {
        int randomAttack = Random.Range(1, 4);

        if (!HasAnimation("Attack03"))
        {
            randomAttack = Random.Range(1, 3);
        }

        string attackAnimation = $"Attack0{randomAttack}";

        if (HasAnimation(attackAnimation))
        {
            animator.Play(attackAnimation, -1, 0f); // 현재 활성화된 레이어에서 "Attack" 애니메이션을 처음부터 재생
        }
    }

    bool HasAnimation(string animationName)
    {
        return animator.HasState(0, Animator.StringToHash(animationName));
    }
    #endregion

    public override void Init()
    {
        base.Init();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Managers.CSV.OnLoaded += UpdateCreatureData;
    }

    public virtual void OnEnable()
    {
        if(Managers.CSV.Data.Count > 0)
        UpdateCreatureData();
    }

    void UpdateCreatureData()
    {
        string goName = gameObject.name.Replace("(Clone)", "");

        foreach (var row in Managers.CSV.Data)
        {
            // 첫 번째 행은 헤더이므로 건너뛰기
            if (row[0] == "Name") continue;

            if (row[0] == goName)
            {
                Name = row[0];
                Grade = row[1];
                Speed = float.Parse(row[2]);
                Health = int.Parse(row[3]);
                MaxHealth = int.Parse(row[3]);
            }
        }

    }

    public virtual void OnDamaged(BaseController attacker, int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
        TakeHit();

        if (Health <= 0)
        {
            Health = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
       
    }



    public void TakeHit()
    {
        StartCoroutine(HitEffectCoroutine());
    }

    private IEnumerator HitEffectCoroutine()
    {
        Color originalColor = spriteRenderer.color;
        Color hitColor = Color.red;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(.2f);
        spriteRenderer.color = originalColor;
    }
}
