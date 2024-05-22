using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace LightCycleSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public TMP_Text livesText;
        public TMP_Text scoreText;
        public TMP_Text gameOverText; 

        private int lives = 3;
        private int score = 0;
        private int lightCycleDestroyedCount = 0;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            gameOverText.gameObject.SetActive(false);
            UpdateLivesText();
            UpdateScoreText();
        }

        public void StartGame()
        {
            // Reset or initialize any game state here
            gameOverText.gameObject.SetActive(false);
            lives = 3;
            score = 0;
            UpdateLivesText();
            UpdateScoreText();
        }

        public void LoseLife()
        {
            lives--;
            UpdateLivesText();

            if (lives <= 0)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "Game Over";
                Invoke(nameof(LoadMainMenu), 3f); 
            }
            else
            {
                ResetGame();
            }
        }

        public void OpponentDestroyed()
        {
            score++;
            UpdateScoreText();
        }

        private void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void UpdateLivesText()
        {
            if (livesText != null)
            {
                livesText.text = "Lives: " + lives;
            }
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }
}
