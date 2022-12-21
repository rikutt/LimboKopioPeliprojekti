using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Supposed to have animation states?, input states, grounded check, wallhug checks etc.

namespace Barebones2D
{
    public class PlayerManager : MonoBehaviour
    {

        public Rigidbody2D RigidBod;
        public Collider2D PlayerCollider2D;
        public Vector2 MovementDirectionVector2;
        public float JumpButtonValue;
        public bool IsDashing;
        public bool IsGrounded;
        public bool IsTouchingLeftWall;
        public bool IsTouchingRightWall;

        
        [SerializeField] private LayerMask platforms;
        [SerializeField] private Transform groundBox;
        [SerializeField] private Transform leftWallBox;
        [SerializeField] private Transform rightWallBox;


        void Start()
        {
            RigidBod = GetComponent<Rigidbody2D>();
            PlayerCollider2D = GetComponent<CapsuleCollider2D>();
            groundBox = transform.Find("GroundCheckBox");
            leftWallBox = transform.Find("LeftWallCheckBox");
            rightWallBox = transform.Find("RightWallCheckBox");
            platforms = LayerMask.GetMask("Platforms");
        }

        void OnMovementAction(InputValue value) => MovementDirectionVector2 = value.Get<Vector2>();
    
        void OnJumpAction(InputValue value) => JumpButtonValue = value.Get<float>();

        void Update()
        {
            IsGrounded = GroundAndWallCheck(groundBox);
            IsTouchingLeftWall = GroundAndWallCheck(leftWallBox);
            IsTouchingRightWall = GroundAndWallCheck(rightWallBox);

        }

        bool GroundAndWallCheck(Transform ColliderObject)
        {
            if (Physics2D.OverlapBox(ColliderObject.position, ColliderObject.localScale, ColliderObject.rotation.z, platforms))
            {
                return true;
            }
            return false;
        }
    }
}
