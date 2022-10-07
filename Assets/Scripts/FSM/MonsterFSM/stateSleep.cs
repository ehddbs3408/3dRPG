using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateSleep : State<MonsterFSM>
{
    private Animator animator;

    protected float healingPoint = 5.0f;

    protected int hashSleep = Animator.StringToHash("Sleep");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
    }
    public override void OnStart()
    {
        animator?.SetBool(hashSleep,true);
    }
    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.TiredPoint -= healingPoint * deltaTime;

        if(stateMachineClass.TiredPoint <= 0)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        animator?.SetBool(hashSleep, false);
    }
}

