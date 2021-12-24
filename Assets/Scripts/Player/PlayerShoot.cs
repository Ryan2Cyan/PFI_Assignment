// Rory Clark - https://rory.games - 2019

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player {
    public class PlayerShoot : MonoBehaviour
    {
        // Objects for firing bullet:
        private PFI_SpaceInvaders_Controller _controlsScript;
        // Shooting:
        private static GameObject BulletPrefab => Resources.Load<GameObject>("Prefabs/Bullet");
        private const float FireRate = 0.2f;
        private float _currentFireTimer;
        // Reloading
        private const int MaxAmmo = 12;
        private int _currentAmmo;
        private float _currentReloadTimer;
        private const float ReloadTime = 1f;
        private Vector3 _bulletSpawnPos;
        public GameObject reloadText;
        // SFX:
        private FMOD.Studio.EventInstance _reloadBulletSfx;
        private FiringMode _currentFiringMode;
        // UI
        public Animator firingModeUI;
        public Slider overheatSlider;




        private void Awake() {
            
            _controlsScript = new PFI_SpaceInvaders_Controller();
            _reloadBulletSfx = FMODUnity.RuntimeManager.CreateInstance("event:/Bullet_Reload");
                
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Fire.performed += Fire;
            _controlsScript.Player.Change_Firing_Mode.performed += ChangeFiringMode;
            
            // Set how much ammo the player will have:
            _currentAmmo = MaxAmmo;
            reloadText.SetActive(false);
        }
        
        private void Update() {
            Debug.Log(_currentFiringMode);
            
            // Set bullet spawn:
            _bulletSpawnPos = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            
            // Decrement firing timer:
            _currentFireTimer -= Time.deltaTime;
            
            // Check if the ammo count has reached 0 - if yes, start reload time:
            if (_currentAmmo == 0) {
                
                _currentReloadTimer += Time.deltaTime;
                reloadText.SetActive(true);
                overheatSlider.value = 0;
                firingModeUI.SetBool("isOverheat", true);
                
                if (!(_currentReloadTimer >= ReloadTime)) return;
                
                _reloadBulletSfx.start();
                _currentAmmo = MaxAmmo;
                _currentReloadTimer = 0f;
                reloadText.SetActive(false);
                firingModeUI.SetBool("isOverheat", false);
            }
            
            // Set overheat to show how many bullets the player has shot:
            SetOverheat();
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
            if (_currentAmmo == 0) return;
            if (!(_currentFireTimer <= 0f)) return;
            
            _currentFireTimer = FireRate;
            SpawnBullet();
            _currentAmmo -= 1;
        }
        
        // Changes firing mode:
        private void ChangeFiringMode(InputAction.CallbackContext context) {
            if (_currentFiringMode == FiringMode.Bullets) {
                _currentFiringMode = FiringMode.Plasma;
                firingModeUI.SetBool("isPlasma", true);
            }
            else {
                _currentFiringMode = FiringMode.Bullets;
                firingModeUI.SetBool("isPlasma", false);
            }
        }
        
        // Spawns bullet prefab on player model:
        private void SpawnBullet() {
            var newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = _bulletSpawnPos;
        }
        
        // Set overheat value:
        private void SetOverheat() {
            overheatSlider.value = MaxAmmo - _currentAmmo;
        }

        private enum FiringMode {
            Bullets, Plasma
        }
    }
}
