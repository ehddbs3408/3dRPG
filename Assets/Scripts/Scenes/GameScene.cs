using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Dictionary<int, Stat> dict = Managers.Data.StatDict;

        /*
        GameObject go = Managers.Resource.Load<GameObject>("Prefabs/Knight");
        Managers.Pool.CreatePool(go, 10);

        Poolable po = Managers.Pool.Pop(go);
        */

        
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 0; ++i)
            list.Add(Managers.Resource.Instantiate("Knight"));
        /*
        for (int i = 0; i < 10; ++i)
        {
            Managers.Resource.Destroy(list[0]);
            list.RemoveAt(0);            
        }
        */
    }

    public override void Clear()
    {
        
    }
}
