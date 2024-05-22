using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightCycleSystem
{
    public class MenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("SampleScene"); // Adjust "SampleScene" to your game scene's name
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "SampleScene") // Again, adjust "SampleScene" as needed
            {
                GameManager.Instance.StartGame();
            }
        }
    }
}
