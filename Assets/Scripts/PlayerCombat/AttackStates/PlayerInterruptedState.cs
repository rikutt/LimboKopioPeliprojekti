using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerInterruptedState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;


        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;
        }
        public void UpdateState() 
        {
            if (playerManagerInstance.MainAttackButtonValue > 0)
            {

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
