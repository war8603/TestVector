using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject _target;
    Camera _camera;
    bool _isHoming = true;
    Vector2 _direction;
    Vector2 _normalized;
    Transform _transform;
    Transform _targetTrans;
    [SerializeField] float _moveSpeed = 10f;

    public void Start()
    {
        _camera = Camera.main;
        _transform = this.transform;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        _targetTrans = target.transform;
        _direction = _targetTrans.position - _transform.position;

        {
            //_normalized = _direction.normalized;
            double length = Math.Sqrt(Math.Pow(_direction.x, 2) + Math.Pow(_direction.y, 2));
            _normalized = new Vector2(_direction.x / (float)length, _direction.y / (float)length);
        }
        
    }

    public void Update()
    {
        if (_target == null)
            return;

        if(_isHoming == false)
        {
            _transform.position = new Vector3(_transform.position.x + _normalized.x * Time.deltaTime * _moveSpeed,
                _transform.position.y + _normalized.y * Time.deltaTime * _moveSpeed,
                _transform.position.z);
        }
        else
        {
            //내적
            Vector2 newDirection = _targetTrans.position - _transform.position;
            float dot = _direction.x * newDirection.x + _direction.y + newDirection.y;
            if(dot == 0) // 평행
            {
                _transform.position = new Vector3(_transform.position.x + _normalized.x * Time.deltaTime * _moveSpeed,
                    _transform.position.y + _normalized.y * Time.deltaTime * _moveSpeed,
                    _transform.position.z);
            }
            else
            {
                //내적.
                float value1 = Vector2.Dot(_direction, newDirection);
                float value2 = (_direction.magnitude * newDirection.magnitude);
                double degree = Mathf.Acos(value1 / value2) * 180 / Math.PI;
                Debug.Log(string.Format("{0} // {1} // {2}", value1, value2, degree));

                Vector2 dir_right = new Vector2(_direction.y, _direction.x * -1);
                double dot_right = _targetTrans.position.x * dir_right.x + _targetTrans.position.y * dir_right.y;

                if (dot_right > 0) // 시계방향 회전
                {

                }
                else //반시계방향 회전
                {
                }
            }
        }
    }
}
