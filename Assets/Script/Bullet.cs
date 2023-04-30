using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    [SerializeField] float _maxDegree = 50f;

    public void Awake()
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
            Vector2 newDirection = _targetTrans.position - _transform.position;
            float dot = _normalized.x * newDirection.x + _normalized.y + newDirection.y;
            Vector3 newPos = new Vector3(_transform.position.x + _normalized.x * Time.deltaTime * _moveSpeed,
                    _transform.position.y + _normalized.y * Time.deltaTime * _moveSpeed,
                    _transform.position.z);
            if (dot == 0) // ∆Ú«‡
            {
                _transform.position = newPos;
            }
            else
            {
                float rad = (float)Math.PI / 180 * _maxDegree;
            }
        }
    }
}
