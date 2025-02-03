using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;

    private void Update()
    {
        float distantion = _speed * Time.deltaTime;
        transform.Translate(_direction * distantion);
    }

    public void Initialize(Vector3 direction)
    {
        _direction = direction.normalized;
    }
}
