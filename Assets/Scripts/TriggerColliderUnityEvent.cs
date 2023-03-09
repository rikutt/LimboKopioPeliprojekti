using UnityEngine;
using UnityEngine.Events;

namespace Barebones2D
{
    public class TriggerColliderUnityEvent : MonoBehaviour
    {
        public UnityEvent OnTriggerEnter;
        public UnityEvent OnTriggerExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnter.Invoke();
            Destroy(gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTriggerExit.Invoke();
        }
    }
}
