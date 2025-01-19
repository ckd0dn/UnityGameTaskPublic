using System.Collections;
using UnityEngine;

public class MonsterController : CreatureController
{
    private Vector3 initPosition;
    private bool isinitPos = false;
    private HpBar hpbar;

    public override void OnEnable()
    {
        base.OnEnable();

        CreatureState = Define.CreatureState.Walk;
        SetPosition();
        hpbar = Managers.Object.Spawn<HpBar>("HpBar.prefab");
    }

    public override void UpdateController()
    {
        base.UpdateController();

        if(hpbar != null)
        {
            hpbar.UpdatePosition(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
            return;

        CreatureState = Define.CreatureState.Attack;
    }

    protected override void UpdateWalk()
    {
        WalkToPlayer();
    }

    void WalkToPlayer()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = new Vector2(Managers.Object.Player.transform.position.x, currentPosition.y);
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        if(hpbar != null) hpbar.UpdateHpBar(MaxHealth, Health);
    }

    protected override void OnDead()
    {
        base.OnDead();

        StartCoroutine(CoDead());
    }

    private IEnumerator CoDead()
    {
        CreatureState = Define.CreatureState.Death;

        yield return new WaitForSeconds(0.5f);

        SpawnNextMonster();
        Managers.Object.Despawn<MonsterController>(this);
        Managers.Object.Despawn<HpBar>(hpbar);
        hpbar = null;
    }

    // 다음 몬스터 소환
    private void SpawnNextMonster()
    {
        for (int i = 1; i < Managers.CSV.Data.Count; i++)
        {
            if (gameObject.name == Managers.CSV.Data[i][0])
            {
                if(i == Managers.CSV.Data.Count - 1)
                {
                    Managers.Object.Spawn<MonsterController>($"{Managers.CSV.Data[1][0]}.prefab");
                }
                else
                {
                    Managers.Object.Spawn<MonsterController>($"{Managers.CSV.Data[i + 1][0]}.prefab");
                }
            }

        }
    }

    // 포지션 세팅
    private void SetPosition()
    {
        if (isinitPos)
        {
            transform.position = initPosition;
        }
        else
        {
            initPosition = transform.position;
            isinitPos = true;
        }
    }

}
