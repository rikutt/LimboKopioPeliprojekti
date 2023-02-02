using UnityEngine;
/*
 * Otetaan vasta t‰‰ll‰ attack suunta, kun sprite facing on implementoitu 
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

            playerCombatStateMachine.BasicWeaponObject.SetActive(true);

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

            if (stateFrameCounter >= attackType.AttackFrames)
            {
                playerCombatStateMachine.SetNextState(new PlayerRecoveryAttackState(attackType));
            }
        }
        public void ExitState()
        {
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;
            playerCombatStateMachine.BasicWeaponObject.SetActive(false);

            // varmuuden vuoks ettei pelaaja j‰‰ hitaaks jos jotain menee vituiks
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;
        }
    }
}
