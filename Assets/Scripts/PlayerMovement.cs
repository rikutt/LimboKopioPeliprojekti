using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Barebones2D
{
    public class PlayerMovement : MonoBehaviour
    {

        public PlayerManager PlayerInstance;

        [SerializeField] private float maxSpeed;
        [SerializeField] private float _accelerationAmount;
        [SerializeField] private float _decelerationAmount;
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private float maxFallWhileTouchingWalls;

        [SerializeField] private int doubleJumpAmount;
        [SerializeField] private float jumpForce;
        [SerializeField] private float wallJumpForce;
        [SerializeField] private float jumpDelayValue;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float gravityJumpScalerTime;

        [SerializeField] private float dashTime;
        [SerializeField] private float dashCooldownTime;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashEndingSpeed;

        private Vector2 _dashVelocity;
        private Vector2 _dashEndingVelocity;
        private int _doubleJumpCounter;
        private float _dashDurationTimer;
        private float _timerFromLastDash;
        private float _coyoteTimerCheck;
        private float _timerFromLastJump;
        private float _gravityScalerTimer;
        private bool hasLetGoOfJump;
        private Vector2 wallJumpDirectionToRight = new Vector2(1f,0.5f);
        private Vector2 wallJumpDirectionToLeft = new Vector2(-1f, 0.5f);

        void FixedUpdate()
        {
            RunTimers();
            HorizontalMovement();
            Jumping();
            ClampFallSpeedAndWallslide();
            Dashing();

        }

        void HorizontalMovement()
        {
            if (PlayerInstance.IsDashing) return;

            float targetSpeed = PlayerInstance.MovementDirectionVector2.x * maxSpeed;

            //when to accelerate/decelerate
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _accelerationAmount : _decelerationAmount;

            // conserve momentum when moving faster than target
            if (Mathf.Abs(PlayerInstance.RigidBod.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(PlayerInstance.RigidBod.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && !PlayerInstance.IsGrounded)
            {
                accelRate = 0;
            }

             // if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)

            float speedDiff = targetSpeed - PlayerInstance.RigidBod.velocity.x;
            float movement = speedDiff * accelRate;

            PlayerInstance.RigidBod.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        //tuntuu et nää if logiikat lähtee käsistä, kun en tehny logiikka pohdintaa tarpeeks etukäteen...
        void Jumping()
        {
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing)
            {
                if (PlayerInstance.IsGrounded || _coyoteTimerCheck > 0 || _doubleJumpCounter > 0 && !PlayerInstance.IsTouchingLeftWall && !PlayerInstance.IsTouchingRightWall)
                {
                    Vector2 velocitySwitch = PlayerInstance.RigidBod.velocity;
                    velocitySwitch.y = jumpForce;
                    PlayerInstance.RigidBod.velocity = velocitySwitch;

                    _timerFromLastJump = jumpDelayValue;
                    _gravityScalerTimer = gravityJumpScalerTime;
                    _coyoteTimerCheck -= coyoteTime;

                    hasLetGoOfJump = false;
                    --_doubleJumpCounter;
                }
            }
            // wall jumps! could need a facing direction comparison
            // LEFT WALL!!!!
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing && !PlayerInstance.IsGrounded && PlayerInstance.IsTouchingLeftWall)
            {
                PlayerInstance.RigidBod.velocity = wallJumpDirectionToRight * wallJumpForce;

                _timerFromLastJump = jumpDelayValue;
                _gravityScalerTimer = gravityJumpScalerTime;

                hasLetGoOfJump = false;
            }
            // RIGHT WALL!!! en jaksa refaktoroida näitä yheks metodiks
            if (PlayerInstance.JumpButtonValue > 0 && _timerFromLastJump < 0 && hasLetGoOfJump && !PlayerInstance.IsDashing && !PlayerInstance.IsGrounded && PlayerInstance.IsTouchingRightWall)
            {
                PlayerInstance.RigidBod.velocity = wallJumpDirectionToLeft * wallJumpForce;

                _timerFromLastJump = jumpDelayValue;
                _gravityScalerTimer = gravityJumpScalerTime;

                hasLetGoOfJump = false;
            }

            // no continuous jumping
            if (PlayerInstance.JumpButtonValue == 0) hasLetGoOfJump = true;

            // decrease gravity when jumping
            if (PlayerInstance.JumpButtonValue > 0 && _gravityScalerTimer > 0)
            {
                PlayerInstance.RigidBod.gravityScale = 0.4f;
            }
            // faster fall
            else if (PlayerInstance.JumpButtonValue == 0 || _gravityScalerTimer < 0)
            {
                PlayerInstance.RigidBod.gravityScale = 3f;
            }
        }

        void OnDashAction(InputValue value)
        {
            // TODO needs to know which side player is facing TODO
            if (PlayerInstance.MovementDirectionVector2 == Vector2.zero) return;

            if (_timerFromLastDash < 0 && !PlayerInstance.IsDashing)
            {
                _dashVelocity = PlayerInstance.MovementDirectionVector2.normalized * dashSpeed;
                _dashEndingVelocity = PlayerInstance.MovementDirectionVector2.normalized * dashEndingSpeed;

                // timers
                _timerFromLastDash = dashCooldownTime;
                _dashDurationTimer = dashTime;
                PlayerInstance.IsDashing = true;
            }
        }
        private void Dashing()
        {
            if (!PlayerInstance.IsDashing) return;
            if (_dashDurationTimer > 0)
            {
                PlayerInstance.RigidBod.velocity = _dashVelocity;
            }
            else if (_dashDurationTimer <= 0)
            {
                PlayerInstance.RigidBod.velocity = _dashEndingVelocity;
                PlayerInstance.IsDashing = false;
            }

        }
        void RunTimers()
        {
            // Being on ground resetting stuff here for some reason
            if (PlayerInstance.IsGrounded && _timerFromLastJump < 0) 
            {
                _coyoteTimerCheck = coyoteTime;
                _doubleJumpCounter = doubleJumpAmount;
            }
            // all timers running down all the time...
            _dashDurationTimer -= Time.deltaTime;
            _timerFromLastDash -= Time.deltaTime;
            _timerFromLastJump -= Time.deltaTime;
            _coyoteTimerCheck -= Time.deltaTime;
            _gravityScalerTimer -= Time.deltaTime;
        }

        void ClampFallSpeedAndWallslide()
        {
            if (PlayerInstance.IsDashing) return;

            if (PlayerInstance.IsTouchingLeftWall || PlayerInstance.IsTouchingRightWall)
            {
                Vector2 fallSpeed = PlayerInstance.RigidBod.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallWhileTouchingWalls, 100f);
                PlayerInstance.RigidBod.velocity = fallSpeed;
            }
            else
            {
                Vector2 fallSpeed = PlayerInstance.RigidBod.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallSpeed, 100f);
                PlayerInstance.RigidBod.velocity = fallSpeed;
            }
        }
        }
    }
