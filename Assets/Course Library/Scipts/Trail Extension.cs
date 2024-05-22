using System.Collections;
using System.Collections;
using UnityEngine;

namespace LightCycleSystem
{
    public class LightCycleTrail : MonoBehaviour
    {
        public GameObject trailSegmentPrefab; // Assign in the Inspector
        public Transform lightCycle;

        private Vector3 lastPosition;
        private GameObject currentTrailSegment;
        private float segmentLength = 0.5f; // Adjust based on your game's scale

        void Awake()
        {
            lastPosition = lightCycle.position;
            CreateNewTrailSegment();
        }

        void Update()
        {
            ExtendCurrentTrailSegment();
        }

        void CreateNewTrailSegment()
        {
            currentTrailSegment = Instantiate(trailSegmentPrefab, lightCycle.position, Quaternion.identity);
            lastPosition = lightCycle.position;
        }

        void ExtendCurrentTrailSegment()
        {
            Vector3 cycleMovement = lightCycle.position - lastPosition;
            if (cycleMovement.magnitude > segmentLength)
            {
                lastPosition = lightCycle.position;
                CreateNewTrailSegment();
            }

            // Adjust the size and position of the collider to cover the new segment of the trail
            Vector3 direction = lightCycle.position - currentTrailSegment.transform.position;
            float distance = direction.magnitude;

            BoxCollider collider = currentTrailSegment.GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y, distance);
            collider.center = new Vector3(0f, 0f, distance / 2f);

            // Orient the trail segment
            currentTrailSegment.transform.LookAt(lightCycle.position);
        }
    }
}
