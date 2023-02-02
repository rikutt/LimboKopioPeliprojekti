using Barebones2D.Animations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

// What most other player scripts need to reference

namespace Barebones2D
{
    public class PlayerManager : MonoBehaviour
    {
        // private set properties. Mukavasti piilottaa turhat pois inspectorista myös :)
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Collider2D Collider2D { get; private set; }
        public Transform SpriteTransform { get; private set; }
        public PlayerAnimations PlayerAnimations { get; private set; }

        // private set Input properties. Törmäsin tohon field:SerializeField ni on täällä muistissa miten laitetaan property näkyviin
        [field:SerializeField] public Vector2 MovementDirectionVector2 { get; private set; }
        public float JumpButtonValue { get; private set; }
        public float MainAttackButtonValue { get; private set; }
        public float DodgeButtonValue { get; private set; }

        // fields
        [NonSerialized] public bool IsFacingLeft;
        [NonSerialized] public bool CanTurnAround = true;
        [NonSerialized] public bool CanDodge; // can be controlled from other scripts without problems
        [NonSerialized] public bool IsDodging; // used for things that can/can't be done while dodging
        [NonSerialized] public bool IsGrounded;
        [NonSerialized] public bool IsTouchingLeftWall, IsTouchingRightWall;
        public bool Invulnerable;

        [Header("Player Movement Speeds")]
        public float MaxMovementSpeed;
        public float AccelerationSpeed;
        public float DecelerationSpeed;

        private LayerMask platforms;
        private Transform groundBox;
        private Transform leftWallBox;
        private Transform rightWallBox;

        void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<CapsuleCollider2D>();
            groundBox = transform.Find("CheckBoxes/GroundCheckBox");
            leftWallBox = transform.Find("CheckBoxes/LeftWallCheckBox");
            rightWallBox = transform.Find("CheckBoxes/RightWallCheckBox");
            platforms = LayerMask.GetMask("Platforms");
            SpriteTransform = transform.Find("Sprites");
            PlayerAnimations = GetComponent<PlayerAnimations>();
        }

        // Setting every player Input value here. Because many scripts want to use them. 
        void OnMovementAction(InputValue value) => MovementDirectionVector2 = value.Get<Vector2>();
    
        void OnJumpAction(InputValue value) => JumpButtonValue = value.Get<float>();

        void OnMainAttackAction(InputValue value) => MainAttackButtonValue = value.Get<float>();

        void OnDodgeAction(InputValue value) => DodgeButtonValue = value.Get<float>();

        // update check grounded/wall collisions
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
