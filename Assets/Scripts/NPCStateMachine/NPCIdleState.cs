using UnityEngine;

namespace Barebones2D.NPC
{
    public class NPCIdleState : IState
    {
        private float IdleTime;
        private bool HasJumped = false;

        public void EnterState(NPCHexagonStateManager NPC)
        {
            
            NPC.HexagonLight.color = new Color(0.3f, 0.6f, 0.95f, 1);
            NPC.InstanceSpriteRenderer.color = new Color(0.3f, 0.6f, 0.95f, 1);
            
        }
        public void UpdateState(NPCHexagonStateManager NPC)
        {
            IdleTime += Time.deltaTime;

            if (!HasJumped && IdleTime >= 1.0f)
            {
                NPC.Rigidbody2D.velocity = NPC.IdleJumpVelocity;

                HasJumped = true;
            }
            if (IdleTime >= 4.0f) 
            {
                NPC.SetState(NPC.PatrolState);
            }
        }
        public void FixedUpdateState(NPCHexagonStateManager NPC)
        {
            
        }
        public void ExitState(NPCHexagonStateManager NPC)
        {
            IdleTime = 0.0f;
            HasJumped = false;
            
        }
    }
}
