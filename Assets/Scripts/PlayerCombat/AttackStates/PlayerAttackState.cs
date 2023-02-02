using UnityEngine;
/*
 * Otetaan vasta täällä attack suunta, kun sprite facing on implementoitu 
 * 
*/
namespace Barebones2D.PlayerCombat
{
    public class PlayerAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        private int stateFrameCounter = 0;

        public PlayerAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }


        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;
            Debug.Log("entered Attack state");

            // todo movement speed lowering with multiplier
            // playerManagerInstance.
        }
        public void UpdateState() 
        {

        }
        public void FixedUpdateState()
        {
            ++stateFrameCounter;

            if (stateFrameCounter >= attackType.AttackFrames)
            {
                playerCombatStateMachine.SetNextState(new PlayerRecoveryAttackState(attackType));
            }
        }
        public void ExitState()
        {
           
        }
    }
}
