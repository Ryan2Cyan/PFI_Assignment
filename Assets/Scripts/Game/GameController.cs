using FMOD.Studio;
using Player;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game {
    public class GameController : MonoBehaviour{
        
        // Player's current score this game:
        private static int _score;
        public static int Score => _score;
        
        // Player's current round this game:
        private static int _round;
        public static int Round => _round;
        
        // Timer to increment the rounds:
        private float _roundTime;
        public float RoundTime => _roundTime;
        
        // When roundTime reaches this number, we move to the next round:
        private const float NextRoundThreshold = 15f;
        
        // Total time the player has survived this run:
        private static float _runTime;
        public static float RunTime => _runTime;

        public GameObject gameOverUI;
        public TextMeshProUGUI gameOverScore;
        public TextMeshProUGUI gameOverWave;
        private float _gameOverTimer;
        private const float GameOverLength = 10f;
        private bool _isGameOver;

        public static void AddScore(int value) {
            _score += value;
        }

        private void Awake() {
            _score = 0;
            _round = 0;
            _isGameOver = false;
        }

        private void Update() {
            _roundTime += Time.deltaTime;

            if (_roundTime >= NextRoundThreshold) {
                _round += 1;
                _roundTime = 0f;
            }

            // Check for game over:
            if (PlayerHealth.CurrentHealth <= 0) {
                _gameOverTimer += Time.deltaTime;
                if (_gameOverTimer >= GameOverLength) {
                    gameOverUI.SetActive(false);
                    BackgroundMusic.BackingTrack.stop(STOP_MODE.IMMEDIATE);
                    SceneManager.LoadScene("Menu");
                }

                if (!_isGameOver) {
                    gameOverScore.text = "Score: " + Score;
                    gameOverWave.text = "Wave: " + Round;
                    gameOverUI.SetActive(true);
                    _isGameOver = true;
                }
            }
        }
        
    }
}
