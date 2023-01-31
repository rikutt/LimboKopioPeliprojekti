using UnityEngine;

// References 
// https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller/blob/main/Scripts/PlayerController.cs
// https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerMovement.cs

namespace Barebones2D.Movement
{
    public class PlayerMovement : MonoBehaviour
    {

        public PlayerManager PlayerInstance;

        [SerializeField] private float maxSpeed, _accelerationAmount, 
                                       _decelerationAmount, maxFallSpeed, 
                                       maxFallWhileTouchingWalls;

        void FixedUpdate()
        {
            HorizontalMovement();
            ClampFallSpeedAndWallslide();

        }

        void HorizontalMovement()
        {
            if (PlayerInstance.IsDodging) return;

            float targetSpeed = PlayerInstance.MovementDirectionVector2.x * maxSpeed;

            //when to accelerate/decelerate
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _accelerationAmount : _decelerationAmount;

            // conserve momentum when moving faster than target
            if (Mathf.Abs(PlayerInstance.Rigidbody2D.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(PlayerInstance.Rigidbody2D.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && !PlayerInstance.IsGrounded)
            {
                accelRate = 0;
            }

             // if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)

            float speedDiff = targetSpeed - PlayerInstance.Rigidbody2D.velocity.x;
            float movement = speedDiff * accelRate;

            PlayerInstance.Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }


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
