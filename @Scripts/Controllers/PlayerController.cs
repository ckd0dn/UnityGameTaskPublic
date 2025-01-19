using System.Collections;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
    [SerializeField] Vector2 detectSize;
    private Coroutine detectCoroutine;

    public override void Init()
    {
        base.Init();

        CreatureState = Define.CreatureState.Idle;
        Damage = 100;
        AttackDelay = 1;
    }

    private void Start()
    {
        detectCoroutine = StartCoroutine(CoDetect());
    }


    #region Detect

    IEnumerator CoDetect()
    {
        while (true)
        {
            Detect();
            yield return new WaitForSeconds(AttackDelay); 
        }
    }

    void Detect()
    {
        var monsterLayer = LayerMask.GetMask("Monster");
        Collider2D hit = Physics2D.OverlapBox(transform.position, detectSize, 0f, monsterLayer);

        if (hit != null)
        {
            var target = hit.GetComponent<MonsterController>();
            if (target == null) return;

            CreatureState = Define.CreatureState.Attack;
            Attack(target);
        }
        else
        {
            CreatureState = Define.CreatureState.Idle;
        }

    }

    #endregion

    #region Attack

    protected override void UpdateAttack()
    {
        base.UpdateAttack();
    }


    void Attack(MonsterController target)
    {
        StartCoroutine(CoAttack(target));
    }

    IEnumerator CoAttack(MonsterController target)
    {
        yield return new WaitForSeconds(0.3f);

        target.OnDamaged(this, Damage);
    }

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position;
        Vector3 boxSize = detectSize * 2;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

}
