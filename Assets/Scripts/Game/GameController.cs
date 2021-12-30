using System;
using Player;
using UnityEngine;

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

        public GameObject gameOverText;

        private static void ResetGame() {
            _score = 0;
            _round = 0;
        }

        public static void AddScore(int value) {
            _score += value;
        }

        private void Awake() {
            ResetGame();
        }

        private void Update() {
            _roundTime += Time.deltaTime;

            if (_roundTime >= NextRoundThreshold) {
                _round += 1;
                _roundTime = 0f;
            }

            // Check for game over:
            if (PlayerHealth.CurrentHealth <= 0) {
                gameOverText.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }
}
