// Rory Clark - https://rory.games - 2019

using System;
using Player.UI;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


namespace Player {
    public class PlayerShoot : MonoBehaviour
    {
        // Actions:
        private PFI_SpaceInvaders_Controller _controlsScript;
        // Shooting:
        private static GameObject BulletPrefab => Resources.Load<GameObject>("Prefabs/Bullet");
        private static GameObject PlasmaPrefab => Resources.Load<GameObject>("Prefabs/Plasma");
        private float _currentFireRate;
        private const float BulletFireRate = 0f;
        private const float PlasmaFireRate = 0.3f;
        private const int BulletAmmoConsumption = 1;
        private const int PlasmaAmmoConsumption = 5;
        private float _currentFireTimer;
        // Reloading
        private static readonly int _maxAmmo = 500;
        public static readonly int MaxAmmo = _maxAmmo;
        private int _currentAmmo;
        private float _currentReloadTimer;
        private const float ReloadTime = 1f;
        private Vector3 _bulletSpawnPos;
        // Ability 1:
        private float _currentAbility1Timer;
        private const float Ability1Cooldown = 10f;
        private const float AbilityActivationTime = 5f;
        private bool _ability1Active;
        public GameObject shipThrusterBig;
        // Ability 2:
        private float _currentAbility2Timer;
        private const float Ability2Cooldown = 30f;
        private const float Ability2ActivationTime = 8f;
        private bool _ability2Active;
        public GameObject laser;
        
        // Current Firing Mode:
        private FiringMode _currentFiringMode;
        private bool _isFiring;
        
       


        private void Awake() {
            _currentFireRate = BulletFireRate;
            _controlsScript = new PFI_SpaceInvaders_Controller();
            _ability1Active = false;
            _ability2Active = false;
            laser.SetActive(false);
            shipThrusterBig.SetActive(false);

            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Fire.performed += SetFireTrue;
            _controlsScript.Player.Fire.canceled += SetFireFalse;
            _controlsScript.Player.Change_Firing_Mode.performed += ChangeFiringMode;
            _controlsScript.Player.Ability_1.performed += Ability1;
            _controlsScript.Player.Ability_2.performed += Ability2;
            
            // Set how much ammo the player will have:
            _currentAmmo = MaxAmmo;
        }
        
        private void Update() {
            
            // Set bullet spawn:
            var position = transform.position;
            _bulletSpawnPos = new Vector3(position.x + 1f, position.y, position.z + Random.Range(-1f, 1f));
            
            // Decrement firing timer:
            _currentFireTimer -= Time.deltaTime;
            
            // Check if the ammo count has reached 0 - if yes, start reload time:
            if (_currentAmmo <= 0) {
                Reload();
            }
            
            // Set overheat to show how many bullets the player has shot:
            FiringModeUI.SetOverheatSlider(MaxAmmo - _currentAmmo);
            
            // Ability Timers:
            Ability1Timer();
            Ability2Timer(); 

            // Shoot on Fire input:
            if (_isFiring) {
                Fire();
                PlayerMovement.DebuffMovementSpeed();
            }
            else {
                PlayerMovement.ResetMovementSpeed();
            }
        }

        private void OnEnable() {
            _controlsScript.Player.Enable();
        }

        private void OnDisable() {
            _controlsScript.Player.Disable();
        }

        private void SetFireTrue(InputAction.CallbackContext context) {
            _isFiring = true;
        }
        private void SetFireFalse(InputAction.CallbackContext context) {
            _isFiring = false;
        }
        
        // Fires bullet on player input:
        private void Fire() {

            // Execute when input is received:
            if (_currentAmmo == 0) return;
            if (!(_currentFireTimer <= 0f)) return;
            
            switch (_currentFiringMode) {
                case FiringMode.Bullets:
                    _currentAmmo -= BulletAmmoConsumption;
                    SpawnBullet(BulletPrefab);
                    break;
                case FiringMode.Plasma:
                    _currentAmmo -= PlasmaAmmoConsumption;
                    SpawnBullet(PlasmaPrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _currentFireTimer = _currentFireRate;
        }
        
        // Changes firing mode:
        private void ChangeFiringMode(InputAction.CallbackContext context) {
            // Change to Plasma:
            if (_currentFiringMode == FiringMode.Bullets) {
                _currentFiringMode = FiringMode.Plasma;
                FiringModeUI.IsPlasmaActive(true);
                _currentFireRate = PlasmaFireRate;
            }
            // Change to Bullets:
            else {
                _currentFiringMode = FiringMode.Bullets;
                FiringModeUI.IsPlasmaActive(false);
                _currentFireRate = BulletFireRate;
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
        private void Ability1Timer() {
            
            // Cooldown for ability 1:
            _currentAbility1Timer += Time.deltaTime;
            Ability1UI.SetCooldownSlider(_currentAbility1Timer);
            if (_currentAbility1Timer >= Ability1Cooldown) {
                Ability1UI.AbilityActive(true);
            }
            
            // Ability 1 Reset:
            if (_currentAbility1Timer >= AbilityActivationTime) {
                PlayerMovement.ResetMovementSpeed();
                if (_ability1Active) {
                    SoundEffects.PlaySfx(SoundEffects.SoundEffectID.Ability1End);
                    _ability1Active = false;
                    shipThrusterBig.SetActive(false);
                    _currentFireRate *= 2f;
                }
            }
        }
        private void Ability1(InputAction.CallbackContext context) {
            
            if (!(_currentAbility1Timer >= Ability1Cooldown)) return;
            
            PlayerMovement.MovementSpeedBuff();
            Ability1UI.AbilityActive(false);
            SoundEffects.PlaySfx(SoundEffects.SoundEffectID.Ability1Start);
            _currentAbility1Timer = 0f;
            _ability1Active = true;
            shipThrusterBig.SetActive(true);
            _currentFireRate /= 2f;
        }
        
        
        // Activate Ability 2 (Mega Laser Beam):
        private void Ability2Timer() {
            
            // Cooldown for ability 1:
            _currentAbility2Timer += Time.deltaTime;
            if (_currentAbility2Timer >= Ability2Cooldown) {
                // Ability is active
            }
            
            // Ability 2 Reset:
            if (_currentAbility2Timer >= Ability2ActivationTime) {
                if (_ability2Active) {
                    SoundEffects.PlaySfx(SoundEffects.SoundEffectID.Ability2End);
                    laser.SetActive(false);
                    _ability2Active = false;
                }
            }
        }
        
        private void Ability2(InputAction.CallbackContext context) {
            
            if (!(_currentAbility2Timer >= Ability2Cooldown)) return;
            Debug.Log("Ability 2 Active");
            // Activated Ability
            laser.SetActive(true);
            SoundEffects.PlaySfx(SoundEffects.SoundEffectID.Ability2Start);
            _currentAbility2Timer = 0f;
            _ability2Active = true;
        }
        

        private enum FiringMode {
            Bullets, Plasma
        }
    }
}
