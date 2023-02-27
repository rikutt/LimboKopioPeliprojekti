using UnityEngine;

namespace Barebones2D
{
    public class EnemyDeaths : MonoBehaviour
    {
        public void OnDeath()
        {
            // can have animation switch, timers to death, disable colliders, etc. etc.
            Destroy(gameObject);
        }
    }
}
