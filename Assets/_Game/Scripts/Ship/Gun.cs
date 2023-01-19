using System;
using UnityEngine;
using Variables;

namespace Ship
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Laser _laserPrefab;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
        
        private void Shoot()
        {
            ObjectPoolManager.SpawnFromPool(_laserPrefab, _transform.position, _transform.rotation);
        }
    }
}
