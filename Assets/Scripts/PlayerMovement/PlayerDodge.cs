using UnityEngine;

// References 
/*
 * paina nappulaa, dodgee input suuntaan tai sprite facing suuntaan. 
 * 
 * 
*/

namespace Barebones2D.Movement
{
    public class PlayerDodge : MonoBehaviour
    {

        private PlayerManager playerManagerInstance;

        [SerializeField] private float dodgeDuration, dodgeCooldown,
                                       dodgeSpeed, dodgeEndingSpeed;

        private float dodgeCooldownTimer;
        private float dodgeDurationTimer;

        private bool dodgeHasReset = true;
        private bool HasLetGoOfDodgeKey = true;

        private Vector2 dodgeVelocity;
        private Vector2 dodgeEndingVelocity;

        // default value is false so set true here
        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
            playerManagerInstance.CanDodge = true;
        }
        private void Update()
        {
            if (dodgeCooldownTimer > 0)
                dodgeCooldownTimer -= Time.deltaTime;
            
            if (dodgeDurationTimer > 0)
                dodgeDurationTimer -= Time.deltaTime;

            // checks for smooth dodge
            if (playerManagerInstance.DodgeButtonValue == 0)
                HasLetGoOfDodgeKey = true;

            if (dodgeDurationTimer <= 0 && !dodgeHasReset && playerManagerInstance.IsGrounded)
                dodgeHasReset = true;

            // try dodging
            if (playerManagerInstance.DodgeButtonValue > 0)
                DodgeStart();
        }

        private void FixedUpdate()
        {
            Dodging();
        }
        
        void DodgeStart()
        {
            // public can dodge check
            // private dodge reset from grounded check
            // IsDodging ni ei voi spammia
            // has let go ni ei voi pit‰‰ pohjassa + buffer feeling
            // cooldown ran down <= 0
            if (!playerManagerInstance.CanDodge)
                return;
            if (!dodgeHasReset)
                return;
            if (playerManagerInstance.IsDodging)
                return;
            if (!HasLetGoOfDodgeKey)
                return;
            if (dodgeCooldownTimer <= 0)
            {
                dodgeCooldownTimer = dodgeCooldown;
                dodgeDurationTimer = dodgeDuration;
                dodgeHasReset = false;
                HasLetGoOfDodgeKey = false;
                playerManagerInstance.IsDodging = true;

                // Set direction and speed for stationary dodge
                if (playerManagerInstance.MovementDirectionVector2 == Vector2.zero)
                {
                    // sprite childs local scale x to dash direction. Not normalized, dont scale x over 1...
                    Vector2 IdleMovementDirectionVector = new Vector2(playerManagerInstance.SpriteTransform.localScale.x, 0);
                    dodgeVelocity = IdleMovementDirectionVector * dodgeSpeed;
                    dodgeEndingVelocity = IdleMovementDirectionVector * dodgeEndingSpeed;
                }
                else
                {
                    dodgeVelocity = playerManagerInstance.MovementDirectionVector2.normalized * dodgeSpeed;
                    dodgeEndingVelocity = playerManagerInstance.MovementDirectionVector2.normalized * dodgeEndingSpeed;
                }
            }
        }

        private void Dodging()
        {
            if (!playerManagerInstance.IsDodging)
                return;

            if (dodgeDurationTimer > 0)
            {
                playerManagerInstance.Rigidbody2D.velocity = dodgeVelocity;
            }
            else
            {
                playerManagerInstance.Rigidbody2D.velocity = dodgeEndingVelocity;
                playerManagerInstance.IsDodging = false;
            }
        }
    }
}