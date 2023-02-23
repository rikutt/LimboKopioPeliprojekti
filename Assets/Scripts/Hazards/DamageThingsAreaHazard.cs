using UnityEngine;

namespace Barebones2D.Hazards
{
    public class DamageThingsAreaHazard : MonoBehaviour
    {
        [SerializeField] private int hazardDamage;
        [SerializeField] private float hazardForceAmountVertical;
        private void OnCollisionEnter2D(Collision2D otherCollider)
        {
            if (otherCollider.gameObject.TryGetComponent(out HealthClass collidedHealth))
            {
                collidedHealth.Health -= hazardDamage;
                otherCollider.rigidbody.AddForce(new Vector2(Random.Range(0, 100), hazardForceAmountVertical));
            }
        }
    }
}
