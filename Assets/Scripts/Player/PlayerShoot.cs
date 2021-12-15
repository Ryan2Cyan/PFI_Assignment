// Rory Clark - https://rory.games - 2019

using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerShoot : MonoBehaviour
    {
        // Objects for firing bullet:
        private PFI_SpaceInvaders_Controller _controlsScript;
        private static GameObject BulletPrefab => Resources.Load<GameObject>("Prefabs/Bullet");
        private const float FireRate = 0.2f;
        private float _currentFireTimer;
        private Vector3 _bulletSpawnPos;
        
        private void Awake() {
            
            _controlsScript = new PFI_SpaceInvaders_Controller();
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Fire.performed += Fire;
        }
        
        private void Update() {
            
            // Set bullet spawn:
            _bulletSpawnPos = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            // Decrement firing timer:
            _currentFireTimer -= Time.deltaTime;
        }
        
        private void OnEnable() {
            _controlsScript.Player.Enable();
        }

        private void OnDisable() {
            _controlsScript.Player.Disable();
        }

        // Fires bullet on player input:
        private void Fire(InputAction.CallbackContext context) {

            // Execute when input is received:
            if (!(_currentFireTimer <= 0f)) return;
            _currentFireTimer = FireRate;
            SpawnBullet();
        }

        private void SpawnBullet() {
            var newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = _bulletSpawnPos;
        }
    }
}
