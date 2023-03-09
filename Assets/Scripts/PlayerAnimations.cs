using UnityEngine;
using UnityEngine.Audio;

/*
 * Laitetaanko myös local scale flippi? varmaan ok täällä
 * tarodev animaatio workflow 
 * https://www.youtube.com/watch?v=ZwLekxsSY3Y
 *  Animator.CrossFade
*/

namespace Barebones2D.Animations
{
    public class PlayerAnimations : MonoBehaviour
    {
        private PlayerManager playerManagerInstance;
        // audio here because im in a hurry and lazy
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip footstepSound;
        [SerializeField] private AudioClip dodgeWhooshSound;
        private float dodgeResetTime = 0.3f;
        private float dodgeTimer;
        [SerializeField] private float footstepTime;
        private float stepsTimer;

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
            //timer
            dodgeTimer -= Time.deltaTime;

            if (isFalling && playerManagerInstance.IsGrounded)
            {
                isFalling = false;
                // anim fallImpact

                //falling hit footstep sound and reset timer
                stepsTimer = footstepTime;
                playerAudioSource.PlayOneShot(footstepSound, 0.5f);
            }
            if (!playerManagerInstance.IsGrounded)
            {
                isFalling = true;
                // anim falling
            }
            if (JustJumped)
            {
                // anim Jump katotaan pitääkö tehä jotain ettei falling animaatio peitä tätä varmaan voi säätää animaattorissa kyl...
                JustJumped = false;

                playerAudioSource.PlayOneShot(footstepSound, 0.5f);
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

                // if on ground play sound
                if (playerManagerInstance.IsGrounded)
                    PlayFootstepSound();
            }

            // run left
            if (playerManagerInstance.MovementDirectionVector2.x < 0 && playerManagerInstance.CanTurnAround)
            {
                
                playerManagerInstance.IsFacingLeft = true;
                playerManagerInstance.SpriteTransform.localScale = new Vector3(-1,1,1);
                // anim running/walking

                // if on ground play sound
                if (playerManagerInstance.IsGrounded)
                    PlayFootstepSound();


            }
            if (playerManagerInstance.IsDodging)
            {
                // anim dodge
                
                PlayDodgeWhooshSound();
            }
            
        }
        void PlayFootstepSound()
        {
            stepsTimer -= Time.deltaTime;
            if (stepsTimer <= 0)
            {
                playerAudioSource.pitch = Random.Range(0.8f, 1.2f);
                playerAudioSource.PlayOneShot(footstepSound, 0.5f);
                stepsTimer = footstepTime;
            }
        }
        void PlayDodgeWhooshSound()
        {
            if (dodgeTimer <= 0)
            {
                //playerAudioSource.pitch = 1.0f;

                playerAudioSource.PlayOneShot(dodgeWhooshSound, 1.0f);
                dodgeTimer = dodgeResetTime;
            }
        }

        void FixedUpdate()
        {

        }
    }
}
