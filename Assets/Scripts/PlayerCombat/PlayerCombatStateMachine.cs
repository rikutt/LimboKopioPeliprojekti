using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* thinking
windup, attack, recovery, interrupted?
2 inputs 2 attacks. 
idle state. 
Both inputs lead through same states? Test first.  

new State sis�ll� stateissa. 
Transitio my�s vaihdossa?

Yield return new WaitForFixedUpdate() 

eli... StateMachine alkaa. Set idleks state machinessa ja aloita updateemaan IdleStatea. Idle updatee, kunnes input tulee. 
Nappi pohjassa, miss� katotaan onko p��stetty irti? Managerissa?
 */

namespace Barebones2D.PlayerCombat
{
    public class PlayerCombatStateMachine : MonoBehaviour
    {
        public IPlayerCombatState CurrentState { get; private set; }

        private IPlayerCombatState nextState;
        private PlayerManager playerManagerInstance;

        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
            nextState = new PlayerIdleCombatState();
            
        }

        private void Update()
        {
            if (nextState != null)
                SetState(nextState);

            if (CurrentState != null) 
                CurrentState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (CurrentState != null)
                CurrentState.FixedUpdateState();
        }

        // State changes below
        public void SetState(IPlayerCombatState _newState)
        {
            nextState = null;

            if (CurrentState != null)
                CurrentState.ExitState();

            CurrentState = _newState;
            CurrentState.EnterState(playerManagerInstance, this);
        }

        public void SetNextState(IPlayerCombatState _nextState)
        {
            if (_nextState != null)
                nextState = _nextState;

        }
    }
}
