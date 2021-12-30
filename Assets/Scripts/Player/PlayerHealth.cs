using System.Collections;
using Camera;
using Sound;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Player {
    public class PlayerHealth : MonoBehaviour {
        
        // Player's current HP:
        private static float _currentHealth;
        public static float CurrentHealth => _currentHealth;

        // Total HP:
        private const int MaxHealth = 100;
        
        // UI Text:
        private Slider _healthSlider;
        
        // Slider Fill:
        private Image _sliderFillImage;
        private Color _defaultFillColor;
        private Color _damageFillColor;
        private Color _lowHealthColor;

        private static bool _isHit;
        
        private void Awake() {
            _isHit = false;
            _healthSlider = transform.GetComponent<Slider>();
            _sliderFillImage = transform.GetChild(1).GetComponent<Image>();
            _currentHealth = MaxHealth;
            _healthSlider.maxValue = MaxHealth;
            _defaultFillColor = _sliderFillImage.color;
            _damageFillColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
            _lowHealthColor = new Color(0.8f, 0.1f, 0.05f, 0.4f);
        }

        // Update is called once per frame
        private void Update() {
            _healthSlider.value = _currentHealth;
            
            if (_isHit) {
                StartCoroutine(ChangeColorOnHit());
                _isHit = false;
            }
        }
        
        public static void DamagePlayer(int damageValue) {
            _currentHealth -= damageValue;
            Utility.ActivateSleep(0.015f);
            if (!(UnityEngine.Camera.main is null))
                UnityEngine.Camera.main.GetComponent<CameraShake>().StartShake(0.2f, 0.6f);
            SoundEffects.PlaySfx(SoundEffects.SoundEffectID.PlayerHurt);
            _isHit = true;
        }

        private IEnumerator ChangeColorOnHit() {
            _sliderFillImage.color = _damageFillColor;
            yield return new WaitForSeconds(0.1f);
            // Change HP bar when at low health:
            _sliderFillImage.color = _currentHealth <= 20 ? _lowHealthColor : _defaultFillColor;
        }

    }
}
