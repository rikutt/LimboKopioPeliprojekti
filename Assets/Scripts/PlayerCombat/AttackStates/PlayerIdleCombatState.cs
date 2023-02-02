using UnityEngine;

// täällä otetaan vastaan inputit ja luodaan State classit oikeineen Attack tyyppeineen

namespace Barebones2D.PlayerCombat
{
    public class PlayerIdleCombatState : IPlayerCombatState
    {
        private PlayerCombatStateMachine playerCombatStateMachine;
        

        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;
        }
        public void UpdateState() 
        {
            if (playerCombatStateMachine.PlayerManagerInstance.IsDodging)
                return;
            // what button and what attack type to send forward
            if (playerCombatStateMachine.PlayerManagerInstance.MainAttackButtonValue > 0 )
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
