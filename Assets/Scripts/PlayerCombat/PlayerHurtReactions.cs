using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerHurtReactions : MonoBehaviour
    {
        private PlayerManager playerManagerInstance;
        private float originalMoveSpeed;

        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
            originalMoveSpeed = playerManagerInstance.MaxMovementSpeed;
        }

        public void PlayerHurt()
        {
            // play animation
            // play sound
            // disable movement things?

        }
        private void Update()
        {
            
        }
    }
}
