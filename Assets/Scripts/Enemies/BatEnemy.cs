using UnityEngine;

// ahhhh... Lepakot patrol asetettuja transformeja
// Tarpeeks l‰hell‰ pelaajaa menee slow moodiin. L‰htee pois kun pelaaja ei ole en‰‰ triggeriss‰. 
// 

namespace Barebones2D
{
    
    public class BatEnemy : MonoBehaviour
    {
        [SerializeField] private int batDamageAmount = 10; 
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float slowingSpeed = 2;
        [SerializeField] private float patrolChangeDistance = 2;
        [SerializeField] private Transform[] patrolPosition;
        private int patrolNumber = 0;

        private Animator animator;
        private Rigidbody2D batRigidbody;
        private Transform currentMoveTarget;
        private bool stateHasStarted;


        private BatState currentState;

        public enum BatState //defaulttina vissiin 0 eli flying?
        {
            Flying,
            Slowing
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            batRigidbody = GetComponent<Rigidbody2D>();
            currentMoveTarget = patrolPosition[patrolNumber];

            // see where first target is
            FlipLocalScale();
        }

        void Update()
        {
            CloseToTargetCheck();
            switch (currentState)
            {
                case BatState.Flying:
                    Flying();
                    break;

                case BatState.Slowing:
                    Slowing();
                    break;

                default:
                    Debug.Log("bat state should never be here???");
                    break;
            }
                    
        }

        private void CloseToTargetCheck()
        {
            Vector2 distanceToTarget = transform.position - currentMoveTarget.position;
            if (Mathf.Abs(distanceToTarget.x) < patrolChangeDistance && Mathf.Abs(distanceToTarget.y) < patrolChangeDistance)
            {
                patrolNumber++;

                if (patrolNumber >= patrolPosition.Length)
                    patrolNumber = 0;

                currentMoveTarget = patrolPosition[patrolNumber];

                FlipLocalScale();
            }
        }

        private void FlipLocalScale()
        {
            if (Mathf.Sign(transform.position.x - currentMoveTarget.position.x) == -1)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);
        }

        void Flying()
        {
            if (!stateHasStarted)
            {
                animator.CrossFade("BatFlying", 0);
                stateHasStarted = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentMoveTarget.position, slowingSpeed * Time.deltaTime);
        }

        void Slowing()
        {
            if (!stateHasStarted)
            {
                animator.CrossFade("BatSlowing", 0);
                stateHasStarted = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentMoveTarget.position, slowingSpeed * Time.deltaTime);
        }

        public void ChangeState()
        {
            // togglaa kahen staten v‰lill‰
            if ((int)currentState == 0)
                currentState = BatState.Slowing;

            else
                currentState = BatState.Flying;


            stateHasStarted = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // outo collider switcharoo pit‰‰ tehd‰?
            GameObject collidedObject = collision.gameObject;

            if (collidedObject.CompareTag("Player"))
            {
                if (collidedObject.TryGetComponent(out HealthClass playerHealth))
                {
                    // Vois laittaa slow systeemin t‰nne. 
                    playerHealth.Health -= batDamageAmount;
                }
            }
        }
    }
}
