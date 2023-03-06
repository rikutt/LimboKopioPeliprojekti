using Unity.VisualScripting;
using UnityEngine;

// jotta damage weaponista toimii pit‰‰ kaikkien collideri2d olla samassa gameobjectissa ku HealthClass


namespace Barebones2D.PlayerCombat
{
    public class BasicWeapon : MonoBehaviour
    {
        [SerializeField] private PlayerCombatStateMachine playerCombatStateMachine;

        private int weaponDamage;

        void Start()
        {
            if (transform.root.TryGetComponent(out PlayerCombatStateMachine StateMachine))
            {
                playerCombatStateMachine = StateMachine;
            }
                
            else
                Debug.LogError("Weapon cant find combatStateMachine");
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if(otherCollider.TryGetComponent(out HealthClass enemyHealth) && !otherCollider.isTrigger)
            {
                // jos paikallaan, tee force IsFacing suuntaan
                if(playerCombatStateMachine.AttackAngle == Vector2.zero && playerCombatStateMachine.PlayerManagerInstance.IsFacingLeft)
                {
                    otherCollider.attachedRigidbody.AddForce(Vector2.left * playerCombatStateMachine.BasicAttack.AttackForce);
                    playerCombatStateMachine.PlayerManagerInstance.Rigidbody2D.AddForce(Vector2.right * playerCombatStateMachine.BasicAttack.AttackForceSelf);
                }
                else if(playerCombatStateMachine.AttackAngle == Vector2.zero && !playerCombatStateMachine.PlayerManagerInstance.IsFacingLeft)
                {
                    otherCollider.attachedRigidbody.AddForce(Vector2.right * playerCombatStateMachine.BasicAttack.AttackForce);
                    playerCombatStateMachine.PlayerManagerInstance.Rigidbody2D.AddForce(Vector2.left * playerCombatStateMachine.BasicAttack.AttackForceSelf);
                }
                else
                {
                    otherCollider.attachedRigidbody.AddForce(playerCombatStateMachine.AttackAngle.normalized * playerCombatStateMachine.BasicAttack.AttackForce);
                    playerCombatStateMachine.PlayerManagerInstance.Rigidbody2D.AddForce(-playerCombatStateMachine.AttackAngle.normalized * playerCombatStateMachine.BasicAttack.AttackForceSelf);
                }
                enemyHealth.Health -= playerCombatStateMachine.BasicAttack.DamageAmount;
            }
        }
    }
}
