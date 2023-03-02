using UnityEngine;
using UnityEngine.Events;

namespace Barebones2D
{
    public class HealthClass : MonoBehaviour
    {
        [SerializeField] int _health = 100;
        [Range(0, 1000)] public int MaxHealth = 200;

        // testing unity events
        public UnityEvent Death;
        public UnityEvent OnTakingDamage;
        public int Health
        {
            get => _health;

            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
                if (_health == 0 ) 
                {
                    // invoke unity event on death, set in editor
                    Death.Invoke();
                }
                else
                {
                    OnTakingDamage.Invoke();
                }
            }
        }

        private void OnValidate()
        {
            Health = _health;
        }
    }
}
