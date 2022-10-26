using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateAttack : State<MonsterFSM>
{
    private Animator animator;

    private StateAttackController stateAttackCtrl;
    private IAttackAble iAttackAble;

    private int hashAttack = Animator.StringToHash("Attack");
    private int hashAttackIdx = Animator.StringToHash("AttackIdx");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();

        stateAttackCtrl = stateMachineClass.GetComponent<StateAttackController>();
        iAttackAble = stateMachineClass.GetComponent<IAttackAble>();
    }

    public override void OnStart()
    {
        if( iAttackAble == null || iAttackAble.nowAttackBehaviour == null )
        {
            stateMachine.ChangeState<stateIdle>();
            return;
        }

        stateAttackCtrl.stateAttackControllerStartHandler -= delegateAttackStart;
        stateAttackCtrl.stateAttackControllerStartHandler += delegateAttackStart;
        stateAttackCtrl.stateAttackControllerEndHandler -= delegateAttackEnd;
        stateAttackCtrl.stateAttackControllerEndHandler += delegateAttackEnd;

        animator?.SetInteger(hashAttackIdx, iAttackAble.nowAttackBehaviour.aniMotionIdx);
        animator?.SetTrigger(hashAttack);
    }

    public override void OnUpdate(float deltaTime)
    {        
    }

    public override void OnHitEvent()
    {
    }

    public void delegateAttackStart()
    {
        Debug.Log("delegateAttackStart");
    }

    public void delegateAttackEnd()
    {
        Debug.Log("delegateAttackEnd");
        stateMachine.ChangeState<stateIdle>();
    }
}
