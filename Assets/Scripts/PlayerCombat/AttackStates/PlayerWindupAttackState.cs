using UnityEngine;


/*
 * kerrotaan player movement attack multiplierilla
 * asetetaan CanTurnAround false
 * katotaan onko paras ottaa direction attackille t‰ss‰ vai attack phasessa
 * 
*/

namespace Barebones2D.PlayerCombat
{
    public class PlayerWindupAttackState : IPlayerCombatState
    {
        private PlayerCombatStateMachine playerCombatStateMachine;
        private MeleeAttackProperties attackType;

        // Asetetaan timer counter 0 niin voi slowata attack speedi‰ kesken lyˆnnin muuttamalla attack type
        private int stateFrameCounter = 0;


        public PlayerWindupAttackState(MeleeAttackProperties _attackType)
        {
            attackType = _attackType;
        }

        public void EnterState(PlayerCombatStateMachine _playerCombatStateMachine)
        {
            playerCombatStateMachine = _playerCombatStateMachine;

            // movement speed multiplier (voi lossata pelin ajalla jotain accuracya mut ei tarpeeks ett‰ v‰li‰ :D
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed *= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed *= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = false;

            // pelaajan movement vectorin (X absolutena Y (-1, 1) v‰lill‰) angle etumerkill‰ verrattuna vector.right(1,0)
            // tulos miinuksena jos kattoo oikeelle... emt...
            playerCombatStateMachine.AttackRotationZ = Vector2.SignedAngle(new Vector2(Mathf.Abs(playerCombatStateMachine.PlayerManagerInstance.MovementDirectionVector2.x), playerCombatStateMachine.PlayerManagerInstance.MovementDirectionVector2.y), Vector2.right);

            if(playerCombatStateMachine.PlayerManagerInstance.IsFacingLeft)
                playerCombatStateMachine.WeaponParentPivot.transform.rotation = Quaternion.AngleAxis(playerCombatStateMachine.AttackRotationZ, Vector3.forward);
            else
                playerCombatStateMachine.WeaponParentPivot.transform.rotation = Quaternion.AngleAxis(-playerCombatStateMachine.AttackRotationZ, Vector3.forward);
        }

        public void UpdateState() 
        {
            if (playerCombatStateMachine.PlayerManagerInstance.IsDodging)
                playerCombatStateMachine.SetNextState(new PlayerIdleCombatState());
        }

        public void FixedUpdateState()
        {
            ++stateFrameCounter;

            if (stateFrameCounter >= attackType.WindupFrames)
            {
                playerCombatStateMachine.SetNextState(new PlayerAttackState(attackType));
            }
        }
        public void ExitState()
        {
            // varmuuden vuoks ettei pelaaja j‰‰ hitaaks jos jotain menee vituiks
            playerCombatStateMachine.PlayerManagerInstance.DecelerationSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.MaxMovementSpeed /= attackType.AttackMoveSpeedMultiplier;
            playerCombatStateMachine.PlayerManagerInstance.CanTurnAround = true;
        }
    }
}
