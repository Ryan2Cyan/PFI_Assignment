using UnityEngine;
using UnityEngine.UI;

namespace Player.UI {
    public class AbilityUI : MonoBehaviour
    {
        private Animator _animator;
        private readonly int IsActive = Animator.StringToHash("isActive");
        private Slider _cooldownSlider;

        private void Awake() {
            _animator = transform.GetComponent<Animator>();
            _cooldownSlider = transform.GetComponent<Slider>();
        }

        public void SetCooldownSlider(float value) {
            _cooldownSlider.value = value;
        }
        
        public void AbilityActive(bool active) {
            _animator.SetBool(IsActive, active);
        }
    }
}
