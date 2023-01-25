using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerIdleCombatState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        

        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerManagerInstance = _playerManagerInstance;
            playerCombatStateMachine = _playerCombatStateMachine;
        }
        public void UpdateState() 
        {
            if (playerManagerInstance.IsDashing)
                return;
            // what button and what attack type to send forward
            if (playerManagerInstance.MainAttackButtonValue > 0 )
            {
                playerCombatStateMachine.SetNextState(new PlayerWindupAttackState(playerCombatStateMachine.BasicAttack));
            }
        }
        public void FixedUpdateState()
        {
            
        }
        public void ExitState()
        {

        }
    }
}
