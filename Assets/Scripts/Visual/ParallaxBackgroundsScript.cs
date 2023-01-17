using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barebones2D
{
    public class ParallaxBackgroundsScript : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] Transform backgroundGroupParent;

        private Vector2 startPosition; 
        private float startPositionZ;

        float distanceFromSubject => transform.position.z - backgroundGroupParent.position.z;
        float clippingPlane => (playerCamera.transform.position.z + (distanceFromSubject > 0 ? playerCamera.farClipPlane : playerCamera.nearClipPlane));

        float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
        Vector2 travel => (Vector2)playerCamera.transform.position - startPosition;
        

        private void Start()
        {
            startPosition = transform.position;
            startPositionZ = transform.position.z;
        }

        private void Update()
        {
            Vector2 newPosition = startPosition + travel * parallaxFactor;
            transform.position = new Vector3(newPosition.x, newPosition.y, startPositionZ);
        }

    }
}
