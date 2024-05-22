using UnityEngine;

namespace LightCycleSystem
{
    public class PlayerCollision : MonoBehaviour
    {
        public GameObject explosionEffectPrefab; // Assign this in the inspector

        private void Start()
        {
            Collider collider = GetComponent<Collider>();
            if (collider == null)
            {
                Debug.LogWarning("Collider component missing on " + gameObject.name);
            }
            else
            {
                collider.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collision is with the player light cycle or other specified tags
            if (other.CompareTag("LightCycle") || // Assuming "LightCycle" is the player tag
                other.CompareTag("WallBarrier") ||
                other.CompareTag("TrailCube") ||
                other.CompareTag("TrailLine"))
            {
                Explode();
                DestroyOpponentsLightCycle();
                // Increment score because an opponent light cycle was destroyed
                GameManager.Instance.OpponentDestroyed();
            }
        }

        private void DestroyOpponentsLightCycle()
        {
            // Destroy the opponent light cycle game object
            Destroy(gameObject);
        }

        private void Explode()
        {
            // Instantiate the explosion effect at the position of the cycle with no rotation
            if (explosionEffectPrefab)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                var parts = explosion.GetComponent<ParticleSystem>();
                if (parts != null)
                {
                    Destroy(explosion, parts.main.duration);
                }
                else
                {
                    Destroy(explosion, 3f);
                }
            }
        }
    }
}
