using UnityEngine;
using UnityEngine.InputSystem;

namespace Barebones2D
{
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBod;
    private float horizontalInput;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float _accelerationAmount;
    [SerializeField] private float _decelerationAmount;
    
    void Awake()
    {
        rigidBod = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HorizontalMovement();    
    }

    void OnHorizontalMove(InputValue value)
    {
        horizontalInput = value.Get<float>();
    }

    void HorizontalMovement() 
    {
        float targetSpeed = horizontalInput * maxSpeed;

        //when to accelerate/decelerate
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _accelerationAmount : _decelerationAmount;
        
        float speedDiff = targetSpeed - rigidBod.velocity.x;
        float movement = speedDiff * accelRate;
        
        rigidBod.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    void OnJump()
    {
        Debug.Log("Jump pressed");
    }
}
}
