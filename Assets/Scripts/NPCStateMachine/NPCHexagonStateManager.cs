using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Barebones2D.NPC
{
    public class NPCHexagonStateManager : MonoBehaviour
    {

        IState currentState;

        public NPCIdleState IdleState = new NPCIdleState();
        public NPCFollowState FollowState = new NPCFollowState();
        public NPCPatrolState PatrolState = new NPCPatrolState();

        // Useful stuff for states to use
        public Rigidbody2D Rigidbody2D;
        public Transform TargetTransform;
        public SpriteRenderer InstanceSpriteRenderer;
        public Light2D HexagonLight;
        public Transform[] PatrolTransforms; 
        public float TargetMoveSpeed, AccelerationRate, PatrolMoveSpeed, PatrolAccelerationRate;
        public Vector2 IdleJumpVelocity;

        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            HexagonLight = GetComponent<Light2D>();
            InstanceSpriteRenderer = GetComponent<SpriteRenderer>();
            currentState = IdleState;

            currentState.EnterState(this);
        }
        void Update()
        {
            currentState.UpdateState(this);
        }
        void FixedUpdate()
        {
            currentState.FixedUpdateState(this);
        }

        // state changes (should?) happen here...
        public void SetState(IState state)
        {
            if (currentState == state) return;

            currentState.ExitState(this);
            currentState = state;
            state.EnterState(this);
        }


        // Player enters/exits trigger collider -> start/exit follow state
        void OnTriggerEnter2D(Collider2D OtherCollider)
        {
            if (currentState == FollowState) return;
            if (OtherCollider.gameObject.tag == "Player")
                SetState(FollowState);
        }
        void OnTriggerExit2D(Collider2D OtherCollider)
        {
            if (currentState == IdleState) return;
            if (OtherCollider.gameObject.tag == "Player")
                SetState(IdleState);
        }

    }
}
