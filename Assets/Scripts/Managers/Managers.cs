using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region CORE
    UIManager _ui = new UIManager();
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    InputManager _input = new InputManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEX _scene = new SceneManagerEX();
    ResourceManager _resource = new ResourceManager();

    public static UIManager UI { get { return Instance._ui; } }
    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static InputManager Input{ get { return Instance._input; } }
    public static SoundManager Sound {  get { return Instance._sound; } }
    public static SceneManagerEX Scene {  get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if( go == null )
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._data.Init();
            s_instance._pool.Init();
        }
    }

    // 씬 전환 시 호출
    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();

        Pool.Clear();
    }
}
