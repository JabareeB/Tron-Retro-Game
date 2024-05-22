using UnityEngine;

namespace LightCycleSystem
{
    public class SpeedPowerUp : MonoBehaviour
    {
        public float speedMultiplier = 2f;
        public float duration = 5f;
        public GameObject particleEffectPrefab; // Reference to your particle effect prefab

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LightCycle"))
            {
                OriginalMove playerMoveScript = other.GetComponent<OriginalMove>();
                if (playerMoveScript != null)
                {
                    playerMoveScript.ActivateSpeedBoost(speedMultiplier, duration);
                    
                    // Instantiate the particle effect at the power-up's position
                    if (particleEffectPrefab != null)
                    {
                        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }
}

