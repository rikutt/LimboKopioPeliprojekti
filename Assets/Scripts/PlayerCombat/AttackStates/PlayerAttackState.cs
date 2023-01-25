using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        private float stateTimer = 0;

        public PlayerAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }


        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerManagerInstance = _playerManagerInstance;
            playerCombatStateMachine = _playerCombatStateMachine;
            Debug.Log("entered Attack state");

            // todo movement speed lowering with multiplier
            // playerManagerInstance.
        }
        public void UpdateState() 
        {
            stateTimer += Time.deltaTime;
        }
        public void FixedUpdateState()
        {
            if (stateTimer >= attackType.AttackTime)
            {
                playerCombatStateMachine.SetNextState(new PlayerRecoveryAttackState(attackType));
            }
        }
        public void ExitState()
        {

        }
    }
}
