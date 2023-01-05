using UnityEngine;


namespace Barebones2D.NPC
{
    public class NPCFollowState : IState
    {
        public void EnterState(NPCHexagonStateManager NPC)
        {
            NPC.HexagonLight.color = new Color(1, 0.2f, 0.2f, 1);
            Debug.Log("Enter state for NPCFollowState");
        }
        public void UpdateState(NPCHexagonStateManager NPC)
        {
            // Debug.Log("test");
        }
        public void FixedUpdateState(NPCHexagonStateManager NPC)
        {
            // move at target
            float finalSpeed;
            float direction = NPC.TargetTransform.position.x - NPC.Rigidbody2D.position.x;
            
            if (direction <= -1.2f) finalSpeed = NPC.TargetMoveSpeed * -1;
            else if (direction >= 1.2f) finalSpeed = NPC.TargetMoveSpeed;
            else return;

            float speedDiff = finalSpeed - NPC.Rigidbody2D.velocity.x;
            float movement = speedDiff * NPC.AccelerationRate;

            NPC.Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
        public void ExitState(NPCHexagonStateManager NPC)
        {
            Debug.Log("Exit state for NPCFollowState");
        }
    }
}
