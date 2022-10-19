using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateRomming : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    private MonsterFSM monsterFSM;

    protected int hashMove = Animator.StringToHash("Move");
    protected int hashAttack = Animator.StringToHash("Attack");
    protected int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnAwake()
    {
        monsterFSM = stateMachineClass as MonsterFSM;

        animator = monsterFSM.GetComponent<Animator>();
        characterController = monsterFSM.GetComponent<CharacterController>();
        agent = monsterFSM.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        Transform posTarget = null;
        if( monsterFSM.posTarget == null )
        {
            posTarget = monsterFSM.SearchNextTargetPosition();
        }
        else
        {
            posTarget = monsterFSM.posTarget;
        }

        agent.stoppingDistance = 0.0f;
        if ( posTarget )
        {
            agent?.SetDestination(posTarget.position);
            animator?.SetBool(hashMove, true);
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.TiredPoint += stateMachineClass.TiredIncrement * deltaTime;

        Transform target = monsterFSM.SearchEnemy();
        if (target)
        {
            if (monsterFSM.GetFlagAttack)
            {
                stateMachine.ChangeState<stateAttack>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
        else
        {
            if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance + 0.001f))
            {
                Transform posTarget = monsterFSM.SearchNextTargetPosition();
                if( posTarget != null )
                {
                    agent?.SetDestination(posTarget.position);
                }
                stateMachine.ChangeState<stateIdle>();
            }
            else
            {
                characterController.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);
            }
        }
    }

    public override void OnEnd()
    {
        agent?.ResetPath();
        agent.stoppingDistance = monsterFSM.attackRange;
    }
}
