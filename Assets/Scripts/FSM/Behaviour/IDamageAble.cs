using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    bool GetFlagLive
    {
        get;
    }

    void SetDamage(int dmg, GameObject prefabEffect);
}
