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
        public float WindupTime { get; private set; }
        public float AttackTime { get; private set; }
        public float RecoveryTime { get; private set; }
        public float AttackMoveSpeedMultiplier { get; private set; }
        #endregion

        public MeleeAttackProperties(
            InterruptibilityEnum _interruptibility, 
            int _damageAmount, 
            float _horizontalColliderMultiplier, 
            float _verticalColliderMultiplier, 
            float _windupTime, 
            float _attackTime, 
            float _recoveryTime,
            float _attackMoveSpeedMultiplier)
        {
            AttackInterruptibility = _interruptibility;
            DamageAmount = _damageAmount;
            HorizontalColliderMultiplier = _horizontalColliderMultiplier;
            VerticalColliderMultiplier = _verticalColliderMultiplier;
            WindupTime = _windupTime;
            AttackTime = _attackTime;
            RecoveryTime = _recoveryTime;
            AttackMoveSpeedMultiplier = _attackMoveSpeedMultiplier;
        }
    }
}
