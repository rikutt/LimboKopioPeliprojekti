using UnityEngine;

namespace Barebones2D.NPC
{
    public class NPCPatrolState : IState
    {
        private float PatrolTargetSwitchTimer;
        private int patrolIndex, patrolIndexLength;
        public void EnterState(NPCHexagonStateManager NPC)
        {
            patrolIndexLength = NPC.PatrolTransforms.Length;
            patrolIndex = 0;
            NPC.HexagonLight.color = new Color(0.7f, 0.78f, 0.97f, 1);
            Debug.Log("Enter state for NPCPatrolState");
        }
        public void UpdateState(NPCHexagonStateManager NPC)
        {
            if (PatrolTargetSwitchTimer >= 3f) 
            {
                ++patrolIndex;
                PatrolTargetSwitchTimer = 0;

                if (patrolIndex == (patrolIndexLength)) 
                    patrolIndex = 0;
            }
        }
        public void FixedUpdateState(NPCHexagonStateManager NPC)
        {
            MoveAtPatrolTarget(NPC.PatrolTransforms[patrolIndex], NPC);
            
            // aivot pls. Array patrol targets length. Miss‰ lis‰‰... Updatessa? jos aika vaiha seuraavaan ja resettaa timer?
        }
        public void ExitState(NPCHexagonStateManager NPC)
        {
            Debug.Log("Exit state for NPCPatrolState");
        }
        void MoveAtPatrolTarget(Transform patrolTarget, NPCHexagonStateManager NPC)
        {
            // move at target
            float finalSpeed;
            float direction = patrolTarget.position.x - NPC.Rigidbody2D.position.x;

            if (direction <= -1.5f) 
                finalSpeed = NPC.PatrolMoveSpeed * -1;

            else if (direction >= 1.5f) 
                finalSpeed = NPC.PatrolMoveSpeed;

            else
            {
                PatrolTargetSwitchTimer += Time.deltaTime;
                return;
            }
            
            float speedDiff = finalSpeed - NPC.Rigidbody2D.velocity.x;
            float movement = speedDiff * NPC.PatrolAccelerationRate;

            NPC.Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
    }
}
