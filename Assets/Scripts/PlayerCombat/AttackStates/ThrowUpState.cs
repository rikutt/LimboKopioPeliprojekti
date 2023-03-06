/*
 * Otetaan vasta t��ll� attack suunta, kun sprite facing on implementoitu 
 * 
*/
namespace Barebones2D.PlayerCombat
{
    public class ThrowUpState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;

        private int stateFrameCounter = 0;
        private int throwupFrameCountDuration = 22;
        

        public ThrowUpState()
        {
            // ei tarvi constructoria t�h�n, tai en jaksa tehd� kunnollista systeemi� joka k�ytt�s t�h�n my�s...
        }

        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;
            
            // hitaampi movespeed eik� sprite voi k��nty�, kun oksentaa karvaa
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed *= 0.5f;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed *= 0.5f;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = false;

            // en osannu instantiate mono behaviouriin ulkopuolelta ni teh��n t�t� kautta...
            playerCombatStateMachine.VomitFur();
        }

        public void UpdateState() 
        {

        }

        public void FixedUpdateState()
        {
            ++stateFrameCounter;

            if (stateFrameCounter >= throwupFrameCountDuration)
            {
                playerCombatStateMachine.SetNextState(new PlayerIdleCombatState());
            }
        }
        public void ExitState()
        {
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;

            // pelaajan movespeed
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed /= 0.5f;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed /= 0.5f;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;
        }
    }
}
