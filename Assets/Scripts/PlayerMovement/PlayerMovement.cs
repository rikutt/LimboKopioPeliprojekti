using UnityEngine;

// References 
// https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller/blob/main/Scripts/PlayerController.cs
// https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerMovement.cs

// has horizontal movement and Clamped fall speed next to walls and not
// TODO air movement speed

namespace Barebones2D.Movement
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private PlayerManager PlayerInstance;

        [SerializeField] private float maxSpeed, accelerationSpeed, 
                                       decelerationSpeed, maxFallSpeed, 
                                       maxFallWhileTouchingWalls;

        [SerializeField] private float airAcceleration, airDeceleration;
        private void Update()
        {
            
        }

        void FixedUpdate()
        {
            HorizontalMovement();
            ClampFallSpeedAndWallslide();
        }

        void HorizontalMovement()
        {
            if (PlayerInstance.IsDodging) return;

            float accelerationAmount;
            float decelerationAmount;

            if (PlayerInstance.IsGrounded)
            {
                accelerationAmount = accelerationSpeed;
                decelerationAmount = decelerationSpeed;
            }
            else
            {
                accelerationAmount = airAcceleration;
                decelerationAmount = airDeceleration;
            }
                

            float targetSpeed = PlayerInstance.MovementDirectionVector2.x * maxSpeed;
            

            //when to accelerate/decelerate
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelerationAmount : decelerationAmount;

            // conserve momentum when moving faster than target
            if (Mathf.Abs(PlayerInstance.Rigidbody2D.velocity.x) > Mathf.Abs(targetSpeed) 
                && Mathf.Sign(PlayerInstance.Rigidbody2D.velocity.x) == Mathf.Sign(targetSpeed) 
                && Mathf.Abs(targetSpeed) > 0.01f && !PlayerInstance.IsGrounded)
            {
                accelRate = 0;
            }

            float speedDiff = targetSpeed - PlayerInstance.Rigidbody2D.velocity.x;
            float movement = speedDiff * accelRate;

            PlayerInstance.Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        // fall speed
        void ClampFallSpeedAndWallslide()
        {
            if (PlayerInstance.IsDodging) return;

            if (PlayerInstance.IsTouchingLeftWall || PlayerInstance.IsTouchingRightWall)
            {
                Vector2 fallSpeed = PlayerInstance.Rigidbody2D.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallWhileTouchingWalls, 100f);
                PlayerInstance.Rigidbody2D.velocity = fallSpeed;
            }
            else
            {
                Vector2 fallSpeed = PlayerInstance.Rigidbody2D.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallSpeed, 100f);
                PlayerInstance.Rigidbody2D.velocity = fallSpeed;
            }
        }
    }
}
