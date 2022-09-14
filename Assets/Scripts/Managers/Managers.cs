using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init();  return instance; } }

    #region CORE
    //Managers
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    SoundManager _sound = new SoundManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEX _scene = new SceneManagerEX();

    //Property
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEX Scene { get { return Instance._scene; } }

    #endregion
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        _input.OnUpdate();
    }


    static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go =  new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            instance._sound.Init();
            instance._data.Init();
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        Input.Clear();
    }
}
