using System;
using Asteroids;
using RuntimeSets;
using UnityEngine;

namespace Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Laser : MonoBehaviour
    {
        [Header("Project References:")] [SerializeField]
        private LaserRuntimeSet _lasers;

        [Header("Values:")]
        [SerializeField] private float _speed = 0.2f;

        [Header("Hit Effect")]
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private LayerMask _hitMask;
        [SerializeField] private LayerMask _effectMask;

        private Rigidbody2D _rigidbody;
        private Transform _transform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        private void OnEnable()
        {
            _lasers.Add(gameObject);
        }

        private void OnDisable()
        {
            _lasers.Remove(gameObject); 
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_transform.position + _transform.up * _speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hitMask == (_hitMask | 1 << other.gameObject.layer))
            {
                if (_effectMask == (_effectMask | 1 << other.gameObject.layer))
                    ObjectPoolManager.SpawnFromPool(_hitEffect, transform.position, Quaternion.identity);

                gameObject.SetActive(false);
            }
        }
    }
}
