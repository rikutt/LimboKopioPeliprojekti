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

        private PlayerManager playerManagerInstance;

        [SerializeField] private float maxFallSpeed, maxFallWhileTouchingWalls;

        
        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
        }

        void FixedUpdate()
        {
            HorizontalMovement();
            ClampFallSpeedAndWallslide();
        }

        void HorizontalMovement()
        {
            if (playerManagerInstance.IsDodging) return;

            float accelerationAmount;
            float decelerationAmount;

            if (playerManagerInstance.IsGrounded)
            {
                accelerationAmount = playerManagerInstance.AccelerationSpeed;
                decelerationAmount = playerManagerInstance.DecelerationSpeed;
            }
            else
            {
                accelerationAmount = playerManagerInstance.AirAcceleration;
                decelerationAmount = playerManagerInstance.AirDeceleration;
            }
                

            float targetSpeed = playerManagerInstance.MovementDirectionVector2.x * playerManagerInstance.MaxMovementSpeed;
            

            //when to accelerate/decelerate
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelerationAmount : decelerationAmount;

            // conserve momentum when moving faster than target
            if (Mathf.Abs(playerManagerInstance.Rigidbody2D.velocity.x) > Mathf.Abs(targetSpeed) 
                && Mathf.Sign(playerManagerInstance.Rigidbody2D.velocity.x) == Mathf.Sign(targetSpeed) 
                && Mathf.Abs(targetSpeed) > 0.01f && !playerManagerInstance.IsGrounded)
            {
                accelRate = 0;
            }

            float speedDiff = targetSpeed - playerManagerInstance.Rigidbody2D.velocity.x;
            float movement = speedDiff * accelRate;

            playerManagerInstance.Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        // fall speed
        void ClampFallSpeedAndWallslide()
        {
            if (playerManagerInstance.IsDodging) return;

            if (playerManagerInstance.IsTouchingLeftWall || playerManagerInstance.IsTouchingRightWall)
            {
                Vector2 fallSpeed = playerManagerInstance.Rigidbody2D.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallWhileTouchingWalls, 100f);
                playerManagerInstance.Rigidbody2D.velocity = fallSpeed;
            }
            else
            {
                Vector2 fallSpeed = playerManagerInstance.Rigidbody2D.velocity;
                fallSpeed.y = Mathf.Clamp(fallSpeed.y, maxFallSpeed, 100f);
                playerManagerInstance.Rigidbody2D.velocity = fallSpeed;
            }
        }
    }
}
