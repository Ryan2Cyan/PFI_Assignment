using UnityEngine;
using UnityEngine.UI;

namespace Player.UI {
    public class Ability1UI : MonoBehaviour
    {
        private static Animator _animator;
        private static readonly int IsActive = Animator.StringToHash("isActive");
        private static Slider _cooldownSlider;

        private void Awake() {
            _animator = transform.GetComponent<Animator>();
            _cooldownSlider = transform.GetComponent<Slider>();
        }
        
        public static void SetCooldownSlider(float value) {
            _cooldownSlider.value = value;
        }
        
        public static void AbilityActive(bool active) {
            _animator.SetBool(IsActive, active);
        }
    }
}
