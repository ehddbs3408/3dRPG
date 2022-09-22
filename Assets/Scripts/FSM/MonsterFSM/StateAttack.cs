using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttack : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;
    private Transform monsterTrm;

    private int hashMove = Animator.StringToHash("Move");
    private int hashAttack = Animator.StringToHash("Attack");
    private int hashTraget = Animator.StringToHash("Target");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();
        monsterTrm = stateMachineClass.GetComponent<Transform>();
    }
    public override void OnStart()
    {
        animator?.SetTrigger(hashAttack);
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();

        if(target)
        {
            Vector3 dir = target.transform.position - monsterTrm.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            monsterTrm.rotation = Quaternion.Lerp(monsterTrm.rotation, quat, 20 * deltaTime);
        }
        else
        {
            stateMachine.ChangeState<StateIdle>();
        }
    }

    public override void OnHitEvent()
    {
        stateMachine.ChangeState<StateIdle>();
    }
}
