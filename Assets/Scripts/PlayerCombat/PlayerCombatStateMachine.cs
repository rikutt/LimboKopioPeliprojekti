using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Pyˆritt‰‰ Attackeihin kuuluvia asioita State Machinena
 * Luo eri attackit ja antaa mahdollisuuden muokata niit‰
 * Default statea lukuunottamatta Statet vaihtuvat Statejen sis‰lt‰
 * 
 * 
thinking:
windup, attack, recovery, interrupted?
2 inputs 2 attacks. 
idle state. 
Both inputs lead through same states? Test first.  

new State sis‰ll‰ stateissa. 
Transitio myˆs vaihdossa?

Yield return new WaitForFixedUpdate() 

eli... StateMachine alkaa. Set idleks state machinessa ja aloita updateemaan IdleStatea. Idle updatee, kunnes input tulee. 
Nappi pohjassa, miss‰ katotaan onko p‰‰stetty irti? Managerissa?

State timerit huvikseen fixed update frameina. Fixed update about 60/s 
Perus tappelupeli tempo pohjalla menn‰‰n. 

 */

namespace Barebones2D.PlayerCombat
{
    public class PlayerCombatStateMachine : MonoBehaviour
    {
        [field:SerializeField] public GameObject BasicWeaponObject { get; private set; }
        [field: SerializeField] public GameObject WeaponParentPivot { get; private set; }
        public IPlayerCombatState CurrentState { get; private set; }
        public PlayerManager PlayerManagerInstance { get; private set; }


        private IPlayerCombatState nextState;

        // temp kunnes keksin paremman tavan luoda/s‰ilytt‰‰ attack dataa
        public MeleeAttackProperties BasicAttack;

        public float AttackRotationZ;
        

        private void Start()
        {
            PlayerManagerInstance = GetComponent<PlayerManager>();
            nextState = new PlayerIdleCombatState();

            // temp kunnes keksin paremman tavan luoda/s‰ilytt‰‰ attack dataa
            BasicAttack = new MeleeAttackProperties(InterruptibilityEnum.Flinchable, 10, 1f, 1f, 10, 5, 5, 0.5f);
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
            CurrentState.EnterState(this);
        }

        // What to call to change States
        public void SetNextState(IPlayerCombatState _nextState)
        {
            if (_nextState != null)
                nextState = _nextState;
        }
    }
}
