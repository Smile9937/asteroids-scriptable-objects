using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {        
        [Header("References:")]
        [SerializeField] private Transform _shape;
        [SerializeField] private AsteroidScriptableObject _stats;

        [SerializeField] private LayerMask _destroyMask;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;

        public void ChangeStats(AsteroidScriptableObject stats) => _stats = stats;

        public void Initialize()
        {
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_destroyMask == (_destroyMask | 1 << other.gameObject.layer))
            {
                if(other.gameObject.CompareTag("Laser"))
                    HitByLaser();

                gameObject.SetActive(false);
            }
        }

        private void HitByLaser()
        {
            if(_stats.ShoudSplit)
            {
                for(int i = 0; i < _stats.SplitCount; i++)
                {
                    Asteroid asteroid = ObjectPoolManager.SpawnFromPool(this, transform.position, Quaternion.identity);

                    asteroid.ChangeStats(_stats.SplitStats);
                    asteroid.Initialize();
                }
            }
        }
        
        private void SetDirection()
        {
            Vector2 size = new Vector2(3f, 3f);
            Vector3 target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            _rigidbody.velocity = Vector3.zero;

            float force = Random.Range(_stats.MinForce, _stats.MaxForce);

            _rigidbody.AddForce(_direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            _rigidbody.angularVelocity = 0;

            float torque = Random.Range(_stats.MinTorque, _stats.MaxTorque);
            int roll = Random.Range(0, 2);

            torque = roll == 0 ? -torque : torque;
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            float size = Random.Range(_stats.MinSize, _stats.MaxSize);
            _shape.localScale = new Vector3(size, size, 0f);
        }
    }
}
