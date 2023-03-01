using UnityEngine;

namespace Barebones2D.Hazards
{
    public class DamageThingsAreaHazard : MonoBehaviour
    {
        [SerializeField] private int hazardDamage;
        [SerializeField] private float hazardForceAmountVertical;
        [SerializeField] private float minRandomForceAmountHorizontal;
        [SerializeField] private float maxRandomForceAmountHorizontal;
        private void OnCollisionEnter2D(Collision2D otherCollider)
        {
            if (otherCollider.gameObject.TryGetComponent(out HealthClass collidedHealth))
            {
                collidedHealth.Health -= hazardDamage;
                otherCollider.rigidbody.AddForce(new Vector2(Random.Range(minRandomForceAmountHorizontal, maxRandomForceAmountHorizontal), hazardForceAmountVertical));
            }
        }
    }
}
