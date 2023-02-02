using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barebones2D
{
    public class HealthClass : MonoBehaviour
    {
        [SerializeField] int _health = 100;
        [Range(0, 1000)] public int MaxHealth = 200;

        public int Health
        {
            get => _health;

            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
                if (_health == 0 ) 
                {
                    Debug.Log(transform.name + " is dead");
                }
            }
        }
        private void OnValidate()
        {
            Health = _health;
        }
    }
    }
