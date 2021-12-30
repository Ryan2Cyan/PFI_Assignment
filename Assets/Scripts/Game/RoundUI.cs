using TMPro;
using UnityEngine;

namespace Game {
    public class RoundUI : MonoBehaviour
    {
        private TextMeshProUGUI _roundText;
        private void Awake() {
            _roundText = transform.GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            _roundText.text = "Wave " + GameController.Round;
        }
    }
}
