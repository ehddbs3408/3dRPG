using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _rotate = 20.0f;

    // 애니메이터
    Animator anim;

    Define.State _state = Define.State.Idle;

    Vector3 _destPos;   // 목적지 위치
    int _mask = (1 << (int)Define.Layer.Ground);

    void Start()
    {
        anim = GetComponent<Animator>();

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

    }

    void Update()
    {
        switch(_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
        }
    }

    void UpdateIdle() 
    {
        anim.SetFloat("Movement", 0);
    }
    void UpdateMoving() 
    {
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;

        if(dir.magnitude < 0.001f)
        {
            _state = Define.State.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _rotate * Time.deltaTime);
        }
        anim.SetFloat("Movement", dir.magnitude);
    }
    void UpdateDie() 
    {
    }

    GameObject prefab = null;
    void OnKeyboard()
    {
        Vector3 dir = Vector3.zero;
        if( Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            dir = Vector3.forward * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            dir = Vector3.back * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            dir = Vector3.left * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            dir = Vector3.right * Time.deltaTime * _speed;
            transform.position += dir;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            //Managers.UI.ShowPopupUI<UI_Button>();
        }

        bool run = false;
        if( Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        anim.SetBool("Run", run);

        if (Input.GetKeyDown(KeyCode.V))
            Managers.UI.ClosePopupUI();

        if (Input.GetKeyDown(KeyCode.F))
            Managers.Sound.Play("Player/univ0001");
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == Define.State.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if( Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            _destPos = hit.point;
            _state = Define.State.Moving;
        }

    }
}
