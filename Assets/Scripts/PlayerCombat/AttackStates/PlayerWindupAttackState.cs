using UnityEngine;

/*
 * kerrotaan player movement attack multiplierilla
 * asetetaan CanTurnAround false
 * katotaan onko paras ottaa direction attackille tässä vai attack phasessa
 * 
*/

namespace Barebones2D.PlayerCombat
{
    public class PlayerWindupAttackState : IPlayerCombatState
    {
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        // Asetetaan timer counter 0 niin voi slowata attack speediä kesken lyönnin muuttamalla attack type
        private int stateFrameCounter = 0;

        public PlayerWindupAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }

        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;
            
            Debug.Log("entered Attack Windup");
        }
        public void UpdateState() 
        {
            if (playerCombatStateMachine.PlayerManagerInstance.IsDodging)
                playerCombatStateMachine.SetNextState(new PlayerIdleCombatState());
        }
        public void FixedUpdateState()
        {
            ++stateFrameCounter;

            if (stateFrameCounter >= attackType.WindupFrames)
            {
                playerCombatStateMachine.SetNextState(new PlayerAttackState(attackType));
            }
        }
        public void ExitState()
        {
            
        }
    }
}
