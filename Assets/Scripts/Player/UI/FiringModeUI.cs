using UnityEngine;
using UnityEngine.UI;

namespace Player {
    public class FiringModeUI : MonoBehaviour
    {
        private static Animator _animator;
        private static Slider _overheatSlider;
        private static readonly int IsOverheat = Animator.StringToHash("isOverheat");
        private static readonly int IsPlasma = Animator.StringToHash("isPlasma");
    
   
        private void Start() {
            _overheatSlider = transform.GetComponent<Slider>();
            _animator = transform.GetComponent<Animator>();
        }

        public static void ResetOverheatSlider() {
            _overheatSlider.value = 0;
        }

        public static void IsOverheating(bool overheat) {
            _animator.SetBool(IsOverheat, overheat);
        }
        
        public static void IsPlasmaActive(bool isActive) {
            _animator.SetBool(IsPlasma, isActive);
        }

        public static void SetOverheatSlider(int value) {
            _overheatSlider.value = value;
        }
    }
}
