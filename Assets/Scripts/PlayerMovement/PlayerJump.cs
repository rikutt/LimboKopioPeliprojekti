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
        [SerializeField] private PlayerManager PlayerInstance;

        [SerializeField]
        private float jumpForce, wallJumpForce,
                         jumpDelay, coyoteTime,
                         gravityScalerMaxTime;

        [SerializeField] private int doubleJumpAmount;
        private int doubleJumpCounter;

        private float coyoteCheckTimer, jumpDelayTimer, gravityScalerTimer;

        private bool hasLetGoOfJump;

        Vector2 JumpVelocity;

        private void Update()
        {
            if (jumpDelayTimer > 0)
                jumpDelayTimer -= Time.deltaTime;

            if (coyoteCheckTimer > 0)
                coyoteCheckTimer -= Time.deltaTime;

            if (gravityScalerTimer > 0)
                gravityScalerTimer -= Time.deltaTime;

            // grounded and jump spam timer -> reset coyote and jump amount
            if (PlayerInstance.IsGrounded && jumpDelayTimer <= 0)
            {
                doubleJumpCounter = doubleJumpAmount;
                coyoteCheckTimer = coyoteTime;
            }

            // Have to release jump key to jump again
            

        }
        private void FixedUpdate()
        {
            if (PlayerInstance.JumpButtonValue > 0)
                Jumping();
            else
                hasLetGoOfJump = true;

            GravityChangeOnJumpHold();
        }
        void GravityChangeOnJumpHold()
        {
            if (PlayerInstance.JumpButtonValue > 0 && gravityScalerTimer > 0)
                PlayerInstance.Rigidbody2D.gravityScale = 0.4f;

            // faster fall
            else if (PlayerInstance.JumpButtonValue == 0 || gravityScalerTimer < 0)
                PlayerInstance.Rigidbody2D.gravityScale = 3f;

        }
        void Jumping()
        {
            if (PlayerInstance.IsDodging || jumpDelayTimer > 0 || !hasLetGoOfJump)
                return;

            // Normal / walljumps / no jump (normal jump maintains velocity.x)
            if (PlayerInstance.IsTouchingRightWall && !PlayerInstance.IsGrounded)
                JumpVelocity = new Vector2(-1.0f, 0.5f).normalized * wallJumpForce;

            else if (PlayerInstance.IsTouchingLeftWall && !PlayerInstance.IsGrounded)
                JumpVelocity = new Vector2(1.0f, 0.5f).normalized * wallJumpForce;

            else if (PlayerInstance.IsGrounded || coyoteCheckTimer > 0 || doubleJumpCounter > 0)
                JumpVelocity = Vector2.up * new Vector2(PlayerInstance.Rigidbody2D.velocity.x, jumpForce);

            else return;

            PlayerInstance.Rigidbody2D.velocity = JumpVelocity;

            jumpDelayTimer = jumpDelay;
            gravityScalerTimer = gravityScalerMaxTime;
            hasLetGoOfJump = false;
            --doubleJumpCounter;

            // remove coyote jump
            coyoteCheckTimer -= coyoteTime;
        }

        /*void Jumping()
        {
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing)
            {
                if (PlayerInstance.IsGrounded || _coyoteTimerCheck > 0 || _doubleJumpCounter > 0 && !PlayerInstance.IsTouchingLeftWall && !PlayerInstance.IsTouchingRightWall)
                {
                    Vector2 velocitySwitch = PlayerInstance.Rigidbody2D.velocity;
                    velocitySwitch.y = jumpForce;
                    PlayerInstance.Rigidbody2D.velocity = velocitySwitch;

                    _timerFromLastJump = jumpDelayValue;
                    gravityScalerTimer = gravityJumpScalerTime;
                    _coyoteTimerCheck -= coyoteTime;

                    hasLetGoOfJump = false;
                    --_doubleJumpCounter;
                }
            }
            // wall jumps! could need a facing direction comparison
            // LEFT WALL!!!!
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing && !PlayerInstance.IsGrounded && PlayerInstance.IsTouchingLeftWall)
            {
                PlayerInstance.Rigidbody2D.velocity = wallJumpDirectionToRight * wallJumpForce;

                _timerFromLastJump = jumpDelayValue;
                gravityScalerTimer = gravityJumpScalerTime;

                hasLetGoOfJump = false;
            }
            // RIGHT WALL!!! en jaksa refaktoroida näitä yheks metodiks
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing && !PlayerInstance.IsGrounded && PlayerInstance.IsTouchingRightWall)
            {
                PlayerInstance.Rigidbody2D.velocity = wallJumpDirectionToLeft * wallJumpForce;

                _timerFromLastJump = jumpDelayValue;
                gravityScalerTimer = gravityJumpScalerTime;

                hasLetGoOfJump = false;
            }

            // no continuous jumping
            if (PlayerInstance.JumpButtonValue == 0) hasLetGoOfJump = true;

            // decrease gravity when jumping
            if (PlayerInstance.JumpButtonValue > 0 && gravityScalerTimer > 0)
            {
                PlayerInstance.Rigidbody2D.gravityScale = 0.4f;
            }
            // faster fall
            else if (PlayerInstance.JumpButtonValue == 0 || gravityScalerTimer < 0)
            {
                PlayerInstance.Rigidbody2D.gravityScale = 3f;
            }
        }*/

    }
}