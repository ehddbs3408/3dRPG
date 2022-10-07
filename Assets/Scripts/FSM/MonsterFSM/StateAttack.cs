using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateAttack : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;
    private Transform monsterTransform;

    private int hashMove = Animator.StringToHash("Move");
    private int hashAttack = Animator.StringToHash("Attack");
    private int hashTarget = Animator.StringToHash("Target");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();
        monsterTransform = stateMachineClass.GetComponent<Transform>();
    }

    public override void OnStart()
    {
        animator?.SetTrigger(hashAttack);
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();

        if( target )
        {
            Vector3 dir = target.transform.position - monsterTransform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            monsterTransform.rotation = Quaternion.Lerp(monsterTransform.rotation, quat, 20.0f * deltaTime);

            if( stateMachineClass.GetFlagAttack )
            {
                animator?.SetTrigger(hashAttack);
            }
            else
            {
                stateMachine.ChangeState<stateIdle>();
            }
        }
        else
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }

    public override void OnHitEvent()
    {
        stateMachine.ChangeState<stateIdle>();
    }
}
