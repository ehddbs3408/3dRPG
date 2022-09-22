using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    private StateMachine<MonsterFSM> fsmManager;

    public LayerMask targetLayerMask;
    public float eyeSight;
    public Transform target;
    public float attackRange;

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
    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, new StateIdle());
        fsmManager.AddStateList(new StateMove());
        fsmManager.AddStateList(new StateAttack());
    }

    private void Update()
    {
        fsmManager.OnUpdate(Time.deltaTime);
    }

    public void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        fsmManager.OnHitEvent();
    }

    public Transform SearchEnemy()
    {
        target = null;
        Collider[] findTargets = Physics.OverlapSphere(transform.position, eyeSight,targetLayerMask);

        if (findTargets.Length > 0)
            target = findTargets[0].transform;

        return target;
    }
}
