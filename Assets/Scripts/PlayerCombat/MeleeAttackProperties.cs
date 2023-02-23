using UnityEditor;
using UnityEngine;

namespace Barebones2D.PlayerCombat
{
    public enum InterruptibilityEnum
    {
        Flinchable,
        SuperArmor
    }
    public class MeleeAttackProperties
    {
        #region properties
        public InterruptibilityEnum AttackInterruptibility { get; private set; }
        public int DamageAmount { get; private set; }
        public float HorizontalColliderMultiplier { get; private set; }
        public float VerticalColliderMultiplier { get; private set; }
        public int WindupFrames { get; private set; }
        public int AttackFrames { get; private set; }
        public int RecoveryFrames { get; private set; }
        public float AttackMoveSpeedMultiplier { get; private set; }
        public float AttackForce { get; private set; }
        public float AttackForceSelf { get; private set; }
        #endregion

        public MeleeAttackProperties(
            InterruptibilityEnum _interruptibility, 
            int _damageAmount, 
            float _horizontalColliderMultiplier, 
            float _verticalColliderMultiplier, 
            int _windupTime, 
            int _attackTime, 
            int _recoveryTime,
            float _attackMoveSpeedMultiplier,
            float _attackForce = 5000,
            float _attackForceSelf = 500)
        {
            AttackInterruptibility = _interruptibility;
            DamageAmount = _damageAmount;
            HorizontalColliderMultiplier = _horizontalColliderMultiplier;
            VerticalColliderMultiplier = _verticalColliderMultiplier;
            WindupFrames = _windupTime;
            AttackFrames = _attackTime;
            RecoveryFrames = _recoveryTime;
            AttackMoveSpeedMultiplier = _attackMoveSpeedMultiplier;
            AttackForce = _attackForce;
            AttackForceSelf= _attackForceSelf;
        }
    }
}
