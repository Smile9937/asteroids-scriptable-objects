using UnityEngine;
using Variables;

namespace Ship
{
    public class Hull : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int startHealth = 10;

        [Header("Collision Bounce")]
        [SerializeField] private float bounceForce;
        [SerializeField] private LayerMask bounceMask;


        private int health;

        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            health = startHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.Equals(other.gameObject.tag, "Asteroid"))
            {
                health--;

                if(health <= 0)
                {
                    health = 0;
                    Debug.Log("Ship Destroyed!");
                }

                EventManager.InvokeUpdateHealthUI(health);
            }

            if (bounceMask == (bounceMask | 1 << other.gameObject.layer))
            {
                rigidBody.AddForce(other.GetContact(0).normal * bounceForce);
            }
        }
    }
}
