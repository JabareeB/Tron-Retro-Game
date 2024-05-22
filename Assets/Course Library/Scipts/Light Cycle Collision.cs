using UnityEngine;

namespace LightCycleSystem
{
    public class LightCycleCollision : MonoBehaviour
    {
        public GameObject explosionEffectPrefab; // Assign this in the inspector

        private void Start()
        {
            // Ensure the Collider component attached to this GameObject is set to "Is Trigger"
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
            else
            {
                Debug.LogWarning($"Collider component missing on {gameObject.name}.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check for collision with specified objects
            if (other.CompareTag("WallBarrier") ||
                other.CompareTag("TrailCube") ||
                other.CompareTag("TrailLine"))
            {
                Explode(); // Handle the explosion effect
                GameManager.Instance.LoseLife(); // Notify the GameManager that a life should be lost
                Destroy(gameObject); // Destroy this GameObject
            }
        }

        private void Explode()
        {
            // Instantiate the explosion effect at the position of the cycle with no rotation
            if (explosionEffectPrefab)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                
                // If there's a ParticleSystem component, destroy the explosion effect after its duration
                ParticleSystem parts = explosion.GetComponent<ParticleSystem>();
                if (parts != null)
                {
                    Destroy(explosion, parts.main.duration);
                }
                else
                {
                    // If there's no ParticleSystem, just destroy after a default time
                    Destroy(explosion, 3f);
                }
            }
        }
    }
}

