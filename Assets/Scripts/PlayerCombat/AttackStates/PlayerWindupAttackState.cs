using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public class PlayerWindupAttackState : IPlayerCombatState
    {
        private PlayerManager playerManagerInstance;
        private PlayerCombatStateMachine playerCombatStateMachine;
        private IEnumerator coroutine;


        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerManagerInstance = _playerManagerInstance;
            playerCombatStateMachine = _playerCombatStateMachine;
            Debug.Log("entered Attack Windup");

           // coroutine = WaitFixedUpdates()
           // playerCombatStateMachine.StartCoroutine()
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
