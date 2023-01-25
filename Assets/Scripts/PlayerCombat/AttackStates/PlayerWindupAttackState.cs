using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerWindupAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        private float stateTimer = 0;

        public PlayerWindupAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }

        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerManagerInstance = _playerManagerInstance;
            playerCombatStateMachine = _playerCombatStateMachine;
            Debug.Log("entered Attack Windup");

        }
        public void UpdateState() 
        {
            stateTimer += Time.deltaTime;
            if (playerManagerInstance.IsDashing)
                playerCombatStateMachine.SetNextState(new PlayerIdleCombatState());
        }
        public void FixedUpdateState()
        {
            if (stateTimer >= attackType.WindupTime)
            {
                playerCombatStateMachine.SetNextState(new PlayerAttackState(attackType));
            }
        }
        public void ExitState()
        {

        }
    }
}
