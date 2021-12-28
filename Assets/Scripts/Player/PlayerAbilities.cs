using UnityEngine;

namespace Player {
    public class PlayerAbilities : MonoBehaviour
    {
        // Actions:
        private PFI_SpaceInvaders_Controller _controlsScript;

        private void Awake()
        {
            _controlsScript = new PFI_SpaceInvaders_Controller();
        }

        private void OnEnable() {
            _controlsScript.Player.Enable();
        }
        
        private void OnDisable() {
            _controlsScript.Player.Disable();
        }
    }
}
