using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{
    public MeleeAttackCollision collisionMeleeAttack;

    public override void CallAttackMotion(GameObject target = null, Transform posAttackStart = null)
    {
        Collider[] colliders = collisionMeleeAttack?.CheckOverlapBox(targetLayerMask);

        foreach(Collider col in colliders)
        {
            col.gameObject.GetComponent<IDamageAble>()?.SetDamage(attackDamage, attackEffectPrefab);
        }

        nowAttackCoolTime = 0.0f;
    }
}
