using System;
using TMPro;
using UnityEngine;

namespace Game {
    public class ScoreUI : MonoBehaviour {
        
        private TextMeshProUGUI _scoreText;
        private void Awake() {
            _scoreText = transform.GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            _scoreText.text = GameController.Score.ToString();
        }
    }
}
