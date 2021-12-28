// Rory Clark - https://rory.games - 2019

using System;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerShoot : MonoBehaviour
    {
        // Actions:
        private PFI_SpaceInvaders_Controller _controlsScript;
        // Shooting:
        private static GameObject BulletPrefab => Resources.Load<GameObject>("Prefabs/Bullet");
        private static GameObject PlasmaPrefab => Resources.Load<GameObject>("Prefabs/Plasma");
        private float _fireRate = 0.2f;
        private float _currentFireTimer;
        // Reloading
        private const int MaxAmmo = 12;
        private int _currentAmmo;
        private float _currentReloadTimer;
        private const float ReloadTime = 1f;
        private Vector3 _bulletSpawnPos;
        // Abilities:
        public Animator ability1UI;
        private static readonly int IsActive = Animator.StringToHash("isActive");
        // Current Firing Mode:
        private FiringMode _currentFiringMode;



        private void Awake() {
            
            _controlsScript = new PFI_SpaceInvaders_Controller();
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Fire.performed += Fire;
            _controlsScript.Player.Change_Firing_Mode.performed += ChangeFiringMode;
            _controlsScript.Player.Ability_1.performed += Ability1;
            
            // Set how much ammo the player will have:
            _currentAmmo = MaxAmmo;
        }
        
        private void Update() {

            // Set bullet spawn:
            var position = transform.position;
            _bulletSpawnPos = new Vector3(position.x + 1f, position.y, position.z);
            
            // Decrement firing timer:
            _currentFireTimer -= Time.deltaTime;
            
            // Check if the ammo count has reached 0 - if yes, start reload time:
            if (_currentAmmo <= 0) {
                Reload();
            }
            
            // Set overheat to show how many bullets the player has shot:
            FiringModeUI.SetOverheatSlider(MaxAmmo - _currentAmmo);

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

            
            switch (_currentFiringMode) {
                case FiringMode.Bullets:
                    _currentAmmo -= 1;
                    SpawnBullet(BulletPrefab);
                    break;
                case FiringMode.Plasma:
                    _currentAmmo -= 3;
                    SpawnBullet(PlasmaPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _currentFireTimer = _fireRate;
        }
        
        // Changes firing mode:
        private void ChangeFiringMode(InputAction.CallbackContext context) {
            
            // Change to Plasma:
            if (_currentFiringMode == FiringMode.Bullets) {
                _currentFiringMode = FiringMode.Plasma;
                FiringModeUI.IsPlasmaActive(true);
                _fireRate = 0.6f;
            }
            // Change to Bullets:
            else {
                _currentFiringMode = FiringMode.Bullets;
                FiringModeUI.IsPlasmaActive(false);
                _fireRate = 0.2f;
            }
        }
        
        // Reloads the ammo according to a reload time:
        private void Reload() {
            
            _currentReloadTimer += Time.deltaTime;
            FiringModeUI.ResetOverheatSlider();
            FiringModeUI.IsOverheating(true);
            
            // Once the timer reaches the reload threshold time, refill ammo.
            if (!(_currentReloadTimer >= ReloadTime)) return;
            
            _currentAmmo = MaxAmmo;
            _currentReloadTimer = 0f;
            FiringModeUI.IsOverheating(false);
                
            // Play SFX depending on firing mode:
            switch (_currentFiringMode) {
                case FiringMode.Bullets:
                    SoundEffects.PlaySfx(SoundEffects.SoundEffectID.BulletReload);
                    break;
                case FiringMode.Plasma:
                    SoundEffects.PlaySfx(SoundEffects.SoundEffectID.PlasmaReload);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Spawns bullet prefab on player model:
        private void SpawnBullet(GameObject prefab) {
            var newBullet = Instantiate(prefab);
            newBullet.transform.position = _bulletSpawnPos;
        }
        
        // Activate Ability 1 (Temporary Speed Boost):
        private void Ability1(InputAction.CallbackContext context) {
            Debug.Log("Ability 1");
            ability1UI.SetBool(IsActive,true);
        }
        
        // Change input UI based on the current input device used:
        // private void ChangeInputUI() {
        //     ability1InputUI.GetComponent<Image>().sprite = Gamepad.current.IsActuated() ? circleInput : qInput;
        // }

        private enum FiringMode {
            Bullets, Plasma
        }
    }
}
