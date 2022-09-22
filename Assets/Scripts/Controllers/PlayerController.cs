using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _rotate = 20.0f;

    //animator
    Animator _anim;
    

    Define.State _state = Define.State.Idle;
    Vector3 _destPos;
    int _mask = (1 << (int)Define.Layer.Ground);

    private void Start()
    {
        _anim = GetComponent<Animator>();

        Managers.Input.keyAction -= Onkeyborad;
        Managers.Input.keyAction += Onkeyborad;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        if (gameObject.GetComponentInChildren<UI_Base>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }
    GameObject prefab = null;
    private void Onkeyborad()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            Managers.UI.ShowPopuoUI<UI_Button>();
        if (Input.GetKeyDown(KeyCode.V))
            Managers.UI.ClosePopupUI();
        if (Input.GetKeyDown(KeyCode.F))
            Managers.Sound.Play("Sounds/Player/univ0001");
    }

    private void Update()
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

    private void UpdateIdle()
    {
        _anim.SetFloat("Movement", 0);
    }

    private void UpdateMoving()
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
        _anim.SetFloat("Movement", dir.magnitude);
    }

    private void UpdateDie()
    {

    }

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == Define.State.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100.0f,_mask))
        {
            _destPos = hit.point;
            _state = Define.State.Moving;
        }
    }
}
