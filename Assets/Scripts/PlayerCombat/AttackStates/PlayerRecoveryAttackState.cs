using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerRecoveryAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        private float stateTimer = 0;

        public PlayerRecoveryAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }

        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerManagerInstance = _playerManagerInstance;
            playerCombatStateMachine = _playerCombatStateMachine;

            Debug.Log("entered recovery state");
        }
        public void UpdateState() 
        {
            stateTimer += Time.deltaTime;
        }
        public void FixedUpdateState()
        {
            if (stateTimer >= attackType.RecoveryTime)
            {
                playerCombatStateMachine.SetNextState(new PlayerRecoveryAttackState(attackType));
            }
        }
        public void ExitState()
        {

        }
    }
}
