using UnityEngine;
/*
 * Uuden attackin queue? Otetaanko HasLetGoOfAttackButton PlayerManagerista?
 * Testataan JustFrame optiota? :D
 * 
 * 
*/


namespace Barebones2D.PlayerCombat
{
    public class PlayerRecoveryAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        private int stateFrameCounter;

        public PlayerRecoveryAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }

        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;

            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed *= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed *= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = false;
        }
        public void UpdateState() 
        {

        }
        public void FixedUpdateState()
        {
            ++stateFrameCounter;

            if (stateFrameCounter >= attackType.RecoveryFrames)
            {
                playerCombatStateMachine.SetNextState(new PlayerIdleCombatState());
            }
        }
        public void ExitState()
        {
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;
        }
    }
}
