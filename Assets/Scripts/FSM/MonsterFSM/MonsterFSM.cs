using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    private StateMachine<MonsterFSM> fsmManager;
    public StateMachine<MonsterFSM> FsmManager => fsmManager;

    private FieldOfView fov;

    public Transform target => fov.FirstTarget;

    public float attackRange;           // ���� ����

    public Transform[] posTargets;      // �ι��� ��ġ��    
    public Transform posTarget = null;  // ������ �ι� ��ġ
    private int posTargetIdx = 0;

    // �ǰﵵ ���� �Լ�
    public float TiredPoint { get; set; } = 0;
    public float TiredIncrement { get; } = 3.0f;
    public float SleepTime { get; } = 50.0f;


    public bool GetFlagAttack
    {
        get
        {
            if (target == null)
                return false;
            float distance = Vector3.Distance(transform.position, target.position);

            return (distance <= attackRange);
        }
    }

    void Start()
    {
        fov = GetComponent<FieldOfView>();

        fsmManager = new StateMachine<MonsterFSM>(this, new stateRomming());

        //stateIdle stateIdle = new stateIdle();
        //stateIdle.isRomming = true;
        //fsmManager.AddStateList(stateIdle);
        fsmManager.AddStateList(new stateMove());
        fsmManager.AddStateList(new stateAttack());
    }

    void Update()
    {
        fsmManager.OnUpdate(Time.deltaTime);
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        fsmManager.OnHitEvent();
    }

    public Transform SearchEnemy()
    {
        return target;
    }

    public Transform SearchNextTargetPosition()
    {
        posTarget = null;

        if( posTargets.Length > 0 && posTargets.Length > posTargetIdx )
        {
            posTarget = posTargets[posTargetIdx];
        }

        posTargetIdx = (posTargetIdx + 1) % posTargets.Length;

        return posTarget;
    }
}
