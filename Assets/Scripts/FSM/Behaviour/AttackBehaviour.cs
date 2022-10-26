using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public int aniMotionIdx;
    public int importanceAttackNo;
    public int attackDamage;
    public float attackRange = 2f;

    [SerializeField]
    private float attackCoolTime;
    protected float nowAttackCoolTime = 0.0f;

    public bool IsAvailable => nowAttackCoolTime >= attackCoolTime;

    // 공격 이펙스
    public GameObject attackEffectPrefab;

    // 공격 LayerMask
    public LayerMask targetLayerMask;

    void Start()
    {
        // 시작하고 바로 공격 가능
        nowAttackCoolTime = attackCoolTime;
    }

    void Update()
    {
        if( nowAttackCoolTime < attackCoolTime )
        {
            nowAttackCoolTime += Time.deltaTime;
        }
    }

    // target : 공격할 적 // posAttackStart : 발사체 발사 위치
    public abstract void CallAttackMotion(GameObject target = null, Transform posAttackStart = null);
}
