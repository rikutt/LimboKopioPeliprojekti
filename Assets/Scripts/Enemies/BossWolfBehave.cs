using UnityEngine;

/* idle state paikoillaan. Kun havaitsee pelaajan, hyökkää. 
 * Primary hyökkäys rush
 * ja secondary ulvonta.
 * Kun pelaaja pakenee, jatkaa partiointia,
jonka jälkeen takas idle


*/


namespace Barebones2D.Enemies

{
    public class BossWolfBehave : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float patrolMoveSpeed = 2.5f;
        [SerializeField] private float howlTriggerDistance;
        [SerializeField] private float howlTriggerTime;
        [SerializeField] private Transform[] patrolPosition;
        bool stateHasStarted;
        private int patrolNumber = 0;
        private float howlTimer;
        private GameObject Player;
        private Rigidbody2D wolfRigidbody;
        private WolfState currentState;
        private enum WolfState
        {
            Idle,
            Aggro,
            Howl,
            Patrol
        }

        private void TurnAroundCheck()
        {
            if (wolfRigidbody.velocity.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            wolfRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            TurnAroundCheck();
            switch (currentState)
            {
                case WolfState.Idle:
                    // Idle();
                    //   Debug.Log(currentState);
                    break;

                case WolfState.Aggro:
                    Aggro();
                    //  Debug.Log(currentState);
                    break;

                case WolfState.Howl:
                    Howl();
                    break;

                case WolfState.Patrol:
                    Patrol();
                    // Debug.Log(currentState);
                    break;

                default:
                    Debug.Log("bat state should never be here???");
                    break;


            }

        }

        private void Howl()
        {
            if (!stateHasStarted)
            {
               
            }
        }

        private void Patrol()
        {
            float playerXDistanceWolf = patrolPosition[patrolNumber].transform.position.x - transform.position.x;

            if (Mathf.Abs(playerXDistanceWolf) < howlTriggerDistance)
            {

                patrolNumber++;
                if (patrolNumber >= patrolPosition.Length)
                {
                    ChangeState(WolfState.Idle);
                    patrolNumber = 0;
                }

            }

            float moveDirection = Mathf.Sign(playerXDistanceWolf);
            wolfRigidbody.velocity = new Vector2(moveDirection, 0) * patrolMoveSpeed;

        }
        void Aggro()
        {




            float playerXDistanceWolf = Player.transform.position.x - transform.position.x;

            if (Mathf.Abs(playerXDistanceWolf) < howlTriggerDistance)
            {
                howlTimer += Time.deltaTime;
                if (howlTimer >= howlTriggerTime)

                {
                    ChangeState(WolfState.Howl);
                }
            }
            else
            {
                howlTimer = 0;
                float moveDirection = Mathf.Sign(playerXDistanceWolf);
                wolfRigidbody.velocity = new Vector2(moveDirection, 0) * moveSpeed;
            }

        }
        void ChangeState(WolfState state)
        {
            currentState = state;

        }


        private void OnTriggerEnter2D(Collider2D collision)

        {
            if (collision.CompareTag("Player"))
            {
                Player = collision.gameObject;
                ChangeState(WolfState.Aggro);
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ChangeState(WolfState.Patrol);
            }
        }
    }
}
