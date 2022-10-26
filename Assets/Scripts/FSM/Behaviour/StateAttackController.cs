using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackController : MonoBehaviour
{
    public delegate void OnStartStateAttackController();
    public OnStartStateAttackController stateAttackControllerStartHandler;

    public delegate void OnEndStateAttackController();
    public OnEndStateAttackController stateAttackControllerEndHandler;

    public bool GetFlagStateAttackController
    {
        get;
        private set;
    }


    void Start()
    {
        stateAttackControllerStartHandler = new OnStartStateAttackController(StateAttackControllerStart);
        stateAttackControllerEndHandler = new OnEndStateAttackController(StateAttackControllerEnd);
    }

    private void StateAttackControllerStart()
    {
        Debug.Log("Attack Start!");
    }

    private void StateAttackControllerEnd()
    {
        Debug.Log("Attack End!");
    }

    public void EventStateAttackStart()
    {
        GetFlagStateAttackController = true;
        if (stateAttackControllerStartHandler != null)
            stateAttackControllerStartHandler();
    }

    public void EventStateAttackEnd()
    {
        GetFlagStateAttackController = false;
        if (stateAttackControllerEndHandler != null)
            stateAttackControllerEndHandler();
    }

    public void OnCheckAttackCollider(int attackIdx)
    {
        Debug.Log("========= Attack Index : " + attackIdx);
    }
}
