using System;
using System.Collections;
using UnityEngine;

namespace Utilities {
    public class Utility : MonoBehaviour {
        
        private float _pauseTimer;
        private static float _pauseDuration;
        private static bool _isActive;
        
        public static float PercentageFunc(float value, float totalValue) {
            return value / totalValue;
        }

        private void Awake() {
            _isActive = false;
        }
        
        private void Update() {
            if (_isActive) {
                Sleep();
            }

        }

        private void Sleep() {
            Time.timeScale = 0.1f;
            _pauseTimer += Time.deltaTime;
            if (_pauseTimer >= _pauseDuration) {
                Time.timeScale = 1;
                _isActive = false;
                _pauseTimer = 0f;
            }
        }

        public static void ActivateSleep(float duration) {
            _isActive = true;
            _pauseDuration = duration;
        }
    }
}
