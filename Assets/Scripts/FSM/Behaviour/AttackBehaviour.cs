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

    // ���� ���彺
    public GameObject attackEffectPrefab;

    // ���� LayerMask
    public LayerMask targetLayerMask;

    void Start()
    {
        // �����ϰ� �ٷ� ���� ����
        nowAttackCoolTime = attackCoolTime;
    }

    void Update()
    {
        if( nowAttackCoolTime < attackCoolTime )
        {
            nowAttackCoolTime += Time.deltaTime;
        }
    }

    // target : ������ �� // posAttackStart : �߻�ü �߻� ��ġ
    public abstract void CallAttackMotion(GameObject target = null, Transform posAttackStart = null);
}
