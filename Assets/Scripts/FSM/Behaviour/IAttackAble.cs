using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackAble
{
    AttackBehaviour nowAttackBehaviour
    {
        get;
    }

    void OnExecuteAttack(int attackIdx);
}
