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

        public void HurtByBossWolf(bool isHowlAttack, float moveSpeedReduction)
        {

            if (isHowlAttack)
            {

                playerManagerInstance.MaxMovementSpeed = moveSpeedReduction;
            }

            else
            {

            }


        }
    }
}
