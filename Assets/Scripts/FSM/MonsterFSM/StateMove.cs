using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateMove : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");
    private int hashAttack = Animator.StringToHash("Attack");
    private int hashTarget = Animator.StringToHash("Target");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        animator?.SetBool(hashMove, true);
        agent?.SetDestination(stateMachineClass.target.position);
    }

    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.TiredPoint += stateMachineClass.TiredIncrement * deltaTime;

        Transform target = stateMachineClass.SearchEnemy();

        if( target )
        {
            agent.SetDestination(stateMachineClass.target.position);

            if( agent.remainingDistance > agent.stoppingDistance )
            {
                characterController.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);
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

    public override void OnEnd()
    {
        agent?.ResetPath();
    }
}
