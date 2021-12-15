// Rory Clark - https://rory.games - 2019
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Player : MonoBehaviour
    {
        // Objects for movement:
        private const float MovementRate = 700f;
        private PFI_SpaceInvaders_Controller _controlsScript;
        private static Quaternion OriginalRot => Quaternion.Euler(
            55,
            90,
            0);
        
        private Vector2 _moveData;
        public Vector2 MoveData => _moveData;
        
        // Objects for firing bullet:
        private static GameObject BulletPrefab => Resources.Load<GameObject>("Prefabs/Bullet");
        private float _fireRate = 0.25f;
        private float _currentFireTimer;
        

        private void Awake() {
            _controlsScript = new PFI_SpaceInvaders_Controller();
            
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Move.performed += context => _moveData = context.ReadValue<Vector2>();
            _controlsScript.Player.Move.canceled += context => _moveData = context.ReadValue<Vector2>();
            _controlsScript.Player.Fire.performed += Fire;
        }

        private void Update() {
            // Calculate new movement and apply it to player rigidbody component:
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -_moveData.x * MovementRate * Time.deltaTime));
            
            // Rotate player based on movement:
            transform.Rotate(Vector3.up * (_moveData.x * 50f * Time.deltaTime));

            // If player stops moving - return to original rotation:
            if (_moveData != Vector2.zero) return;
            var rotation = transform.rotation;
            rotation = Quaternion.Lerp(Quaternion.Euler(
                    rotation.eulerAngles.x,
                    rotation.eulerAngles.y,
                    rotation.eulerAngles.z),
                OriginalRot, 
                Time.deltaTime);
                
            transform.rotation = rotation;
            
            // Increment firing timer:
            if (_currentFireTimer > 0)
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
            if (context.performed && _currentFireTimer <= 0f) {
                Debug.Log("Fire! " + context.phase);
                _currentFireTimer = _fireRate;
                GameObject go = Instantiate(BulletPrefab);
                go.transform.position = transform.position;
            }
        }
    }
}

