using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    private StateMachine<MonsterFSM> fsmManager;
    private void Start()
    {
        //fsmManager = new StateMachine<MonsterFSM>(this, new StateIdle());
        //fsmManager.AddStateList(new stateMove());
        //fsmManager.AddStateList(new stateAttack());
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
}
