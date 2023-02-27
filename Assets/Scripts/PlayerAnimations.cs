using UnityEngine;

/*
 * Laitetaanko my�s local scale flippi? varmaan ok t��ll�
 * tarodev animaatio workflow 
 * https://www.youtube.com/watch?v=ZwLekxsSY3Y
 *  Animator.CrossFade
*/

namespace Barebones2D.Animations
{
    public class PlayerAnimations : MonoBehaviour
    {
        private PlayerManager playerManagerInstance;

        [SerializeField] private Animation jumping;
        [SerializeField] private Animation running;
        [SerializeField] private Animation dodging;
        [SerializeField] private Animation falling;
        [SerializeField] private Animation fallImpact;

        [Header("BasicAttack")]
        [SerializeField] private Animation attackWindup;
        [SerializeField] private Animation attackSwing;
        [SerializeField] private Animation attackRecovery;

        [Header("Vomit Fur")]
        [SerializeField] private Animation furVomitBallWindup;
        [SerializeField] private Animation furVomitBallAttack;
        [SerializeField] private Animation furVomitBallRecovery;

        private bool isFalling;
        // private bool isIdle;
        public bool JustJumped { get; set; }

        private void Start()
        {
            playerManagerInstance = GetComponent<PlayerManager>();
        }
        private void Update()
        {
            if (isFalling && playerManagerInstance.IsGrounded)
            {
                isFalling = false;
                // anim fallImpact
            }
            if (!playerManagerInstance.IsGrounded)
            {
                isFalling = true;
                // anim falling
            }
            if (JustJumped)
            {
                // anim Jump katotaan pit��k� teh� jotain ettei falling animaatio peit� t�t� varmaan voi s��t�� animaattorissa kyl...
                JustJumped = false;
            }
            if (playerManagerInstance.MovementDirectionVector2.x == 0)
            {
                // anim idle
                
                //SpriteTransform
            }

            // run right
            if (playerManagerInstance.MovementDirectionVector2.x > 0 && playerManagerInstance.CanTurnAround)
            {
                playerManagerInstance.IsFacingLeft = false;
                playerManagerInstance.SpriteTransform.localScale = new Vector3(1, 1, 1);
                // anim running/walking
                
            }

            // run left
            if (playerManagerInstance.MovementDirectionVector2.x < 0 && playerManagerInstance.CanTurnAround)
            {
                
                playerManagerInstance.IsFacingLeft = true;
                playerManagerInstance.SpriteTransform.localScale = new Vector3(-1,1,1);
                // anim running/walking
                
            }
            if (playerManagerInstance.IsDodging)
            {
                // anim dodge
            }
            
        }

        void FixedUpdate()
        {

        }
    }
}
