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
        [field: SerializeField] public Transform FurVomitSpawnPoint { get; private set; }
        [field: SerializeField] public FurVomitProjectile FurVomitProjectileSpawn { get; private set; }

        [SerializeField] private float vomitSpeed;
        public IPlayerCombatState CurrentState { get; private set; }
        public PlayerManager PlayerManagerInstance { get; private set; }



        private IPlayerCombatState nextState;

        // temp kunnes keksin paremman tavan luoda/s‰ilytt‰‰ attack dataa
        public MeleeAttackProperties BasicAttack;
        private FurVomitProjectile lastSpawnedFurVomit;

        public float AttackRotationZ;
        public Vector2 AttackAngle;
        

        private void Start()
        {
            PlayerManagerInstance = GetComponent<PlayerManager>();
            nextState = new PlayerIdleCombatState();

            // temp kunnes keksin paremman tavan luoda/s‰ilytt‰‰ attack dataa
            BasicAttack = new MeleeAttackProperties(InterruptibilityEnum.Flinchable, 5, 1f, 1f, 7, 10, 3, 0.5f, 8000f);
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



        // vomit t‰‰ll‰ koska mono behaviour meh
        public void VomitFur()
        {
            lastSpawnedFurVomit = Instantiate(FurVomitProjectileSpawn, FurVomitSpawnPoint.position, Quaternion.identity);
            Rigidbody2D vomitRigidbody2D = lastSpawnedFurVomit.GetComponent<Rigidbody2D>();

            Vector2 AttackAngle = PlayerManagerInstance.MovementDirectionVector2;
            // pelaajan movement vectorin (X absolutena Y (-1, 1) v‰lill‰) angle etumerkill‰ verrattuna vector.right(1,0)
            // tulos miinuksena jos kattoo oikeelle... emt...

            if (PlayerManagerInstance.IsFacingLeft && AttackAngle == Vector2.zero)
                vomitRigidbody2D.velocity = Vector2.left * vomitSpeed;

            else if (!PlayerManagerInstance.IsFacingLeft && AttackAngle == Vector2.zero)
                vomitRigidbody2D.velocity = Vector2.right * vomitSpeed;

            else
                vomitRigidbody2D.velocity = AttackAngle * vomitSpeed;

        }
    }
}
