using UnityEngine;

namespace Barebones2D
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private GameObject Player;
        [SerializeField] private Transform currentCheckpoint;

        public void SpawnPlayerToCheckpoint()
        {
            // could add effects for death here
            Player.SetActive(true);
            Player.transform.position = currentCheckpoint.position;
            Player.GetComponent<HealthClass>().Health = 50;
        }

        public void ChangeCheckpoint(Transform newTransform)
        {
            currentCheckpoint = newTransform;
        }
    }
}
