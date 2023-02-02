using UnityEngine;

/*
 * JumpButtonValue > 0
 * hasLetGoOfJump
 * dodgeCooldownTimer <= 0
 * IsGrounded or has jump count > 0 or coyote timer
 * Timer from last jump <= 0
 * _coyoteTimerCheck
 * 
 * not touching left/right wall
 * 
 * gravity lowers while holding down jump
*/

namespace Barebones2D.Movement
{
    public class PlayerJump : MonoBehaviour
    {
        private PlayerManager playerManagerInstance;

        [SerializeField] private float jumpForce, wallJumpForce,
                         jumpDelay, coyoteTime,
                         gravityScalerMaxTime;

        [SerializeField] private int doubleJumpAmount;
        private int doubleJumpCounter;

        private bool hasLetGoOfJump;

        private float coyoteCheckTimer, jumpDelayTimer, gravityScalerTimer;

        Vector2 JumpVelocity;

        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (jumpDelayTimer > 0)
                jumpDelayTimer -= Time.deltaTime;

            if (coyoteCheckTimer > 0)
                coyoteCheckTimer -= Time.deltaTime;

            if (gravityScalerTimer > 0)
                gravityScalerTimer -= Time.deltaTime;

            // grounded and jump spam timer -> reset coyote and jump amount
            if (playerManagerInstance.IsGrounded && jumpDelayTimer <= 0)
            {
                doubleJumpCounter = doubleJumpAmount;
                coyoteCheckTimer = coyoteTime;
            }

            GravityChangeOnJumpHold();
        }

        void GravityChangeOnJumpHold()
        {
            if (playerManagerInstance.JumpButtonValue > 0 && gravityScalerTimer > 0)
                playerManagerInstance.Rigidbody2D.gravityScale = 0.4f;

            // faster fall
            else if (playerManagerInstance.JumpButtonValue == 0 || gravityScalerTimer < 0)
                playerManagerInstance.Rigidbody2D.gravityScale = 3f;

        }
        private void FixedUpdate()
        {
            if (playerManagerInstance.JumpButtonValue > 0)
                Jumping();

            else
                hasLetGoOfJump = true;
        }
        void Jumping()
        {
            if (playerManagerInstance.IsDodging || jumpDelayTimer > 0 || !hasLetGoOfJump)
                return;

            // Normal / walljumps / no jump (normal jump maintains velocity.x)
            if (playerManagerInstance.IsTouchingRightWall && !playerManagerInstance.IsGrounded)
                JumpVelocity = new Vector2(-1.0f, 0.6f).normalized * wallJumpForce;

            else if (playerManagerInstance.IsTouchingLeftWall && !playerManagerInstance.IsGrounded)
                JumpVelocity = new Vector2(1.0f, 0.6f).normalized * wallJumpForce;

            else if (playerManagerInstance.IsGrounded || coyoteCheckTimer > 0 || doubleJumpCounter > 0)
                JumpVelocity = new Vector2(playerManagerInstance.Rigidbody2D.velocity.x, jumpForce);

            else return;
            
            playerManagerInstance.Rigidbody2D.velocity = JumpVelocity;

            hasLetGoOfJump = false;
            playerManagerInstance.PlayerAnimations.JustJumped = true;
            --doubleJumpCounter;

            // timers reset
            jumpDelayTimer = jumpDelay;
            gravityScalerTimer = gravityScalerMaxTime;

            // remove coyote jump
            coyoteCheckTimer -= coyoteTime;
        }
    }
}