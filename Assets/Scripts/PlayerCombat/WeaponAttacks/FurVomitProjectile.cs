using UnityEngine;

// jotta damage weaponista toimii pit‰‰ kaikkien collideri2d olla samassa gameobjectissa ku HealthClass


namespace Barebones2D.PlayerCombat
{
    public class FurVomitProjectile : MonoBehaviour
    {
        [SerializeField] private int VomitDamage = 11;
        [SerializeField] private float destroyTime;

        public Rigidbody2D vomitRigidbody { get; private set; }

        void Start()
        {
            vomitRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            destroyTime -= Time.deltaTime;

            if (destroyTime <= 0) 
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // outo collider switcharoo pit‰‰ tehd‰?
            GameObject collidedObject = collision.gameObject;

            if (collidedObject.TryGetComponent(out HealthClass otherHealth))
            {
                // Vois laittaa slow systeemin t‰nne. 
                otherHealth.Health -= VomitDamage;

                Destroy(gameObject);
            }
        }
    }
}
