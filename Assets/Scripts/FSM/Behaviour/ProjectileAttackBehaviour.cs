using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackBehaviour : AttackBehaviour
{
    public override void CallAttackMotion(GameObject target = null, Transform posAttackStart = null)
    {
        if (target == null)
            return;

        Vector3 vecProjectile = posAttackStart?.position ?? transform.position;

        if(attackEffectPrefab != null)
        {
            GameObject objprojectile = GameObject.Instantiate<GameObject>(attackEffectPrefab, vecProjectile, Quaternion.identity);

            objprojectile.transform.forward = transform.forward;

            ProjectileAttackCollision collid = objprojectile.GetComponent<ProjectileAttackCollision>();
            if(collid != null)
            {
                collid.projectileParents = this.gameObject;
                collid.attackBehaviour = this;
                collid.target = target;
            }
        }

        nowAttackCoolTime = 0.0f;
    }
}
