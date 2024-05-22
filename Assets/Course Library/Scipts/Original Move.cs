using System.Collections;
using UnityEngine;

namespace LightCycleSystem
{
    [HelpURL("https://assetstore.unity.com/packages/slug/226235")]
    public class OriginalMove : MonoBehaviour
    {
        [SerializeField] private Transform lightCycle;
        [SerializeField] private float turnSpeed = 5f, turnRotationSpeed = 5f, tiltSpeed = 5f, tiltRotationSpeed = 5f, acceleration = 0.2f, maxSpeed = 10f, groundHeight = 1f;
        private float originalMaxSpeed; // To store the original max speed
        private float speed;
        private float turnInput;
        private bool isInvincible = false; // Track the invincibility state

        void Awake()
        {
            turnInput = 0f;
            speed = 0f;
            originalMaxSpeed = maxSpeed; // Store the original max speed value
        }

        IEnumerator Start()
        {
            while (speed < maxSpeed)
            {
                speed = Mathf.Lerp(speed, maxSpeed + 1f, acceleration * Time.deltaTime);
                yield return null;
            }
        }

        void Update()
        {
            turnInput = Input.GetAxis("Horizontal");
            transform.position += transform.forward * speed * Time.deltaTime;
            float turn = turnInput * turnSpeed * Time.deltaTime;
            transform.Rotate(0, turn * turnRotationSpeed, 0);
            lightCycle.localRotation = Quaternion.Lerp(lightCycle.localRotation, Quaternion.Euler(0, 0, -tiltSpeed * turnInput), tiltRotationSpeed * Time.deltaTime);
            MaintainGroundHeight();
        }

        void MaintainGroundHeight()
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y = hit.point.y + groundHeight;
                transform.position = currentPosition;
            }
        }

        public void ActivateSpeedBoost(float multiplier, float duration)
        {
            StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
        }

        private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
        {
            maxSpeed *= multiplier;
            yield return new WaitForSeconds(duration);
            maxSpeed = originalMaxSpeed;
        }

        public void ActivateInvincibility(float duration)
        {
            StartCoroutine(InvincibilityDuration(duration));
        }

        private IEnumerator InvincibilityDuration(float duration)
        {
            isInvincible = true;
            yield return new WaitForSeconds(duration);
            isInvincible = false;
        }

        // Public method to check the invincibility state
        public bool IsInvincible()
        {
            return isInvincible;
        }
    }
}


