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

        GameObject go = Managers.Resource.Load<GameObject>("Prefabs/Knight");
        Managers.Pool.CreatePool(go, 10);

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            Managers.Resource.Instantiate("Knigh");
        }

        //for(int i = 0;i<10;i++)
        //{
        //    Managers.Resource.Destroy(list[i]);
        //    list.RemoveAt(i);
        //}

        //for (int i = 0; i < 5; i++)
        //{
        //    list.Add(Managers.Resource.Instantiate("Player"));
        //}


    }
    public override void Clear()
    {
        
    }
}
