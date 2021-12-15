// Rory Clark - https://rory.games - 2019

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {
        private const float MovementRate = 700f;
        private PFI_SpaceInvaders_Controller _controlsScript;
        private Quaternion OriginalRot => Quaternion.Euler(
            55,
            90,
            0);
        private Vector2 _moveData;
        public Vector2 MoveData => _moveData;
        

        private void Awake() {
            _controlsScript = new PFI_SpaceInvaders_Controller();
            
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Move.performed += context => _moveData = context.ReadValue<Vector2>();
            _controlsScript.Player.Move.canceled += context => _moveData = context.ReadValue<Vector2>();
        }

        private void Update() {
            // Calculate new movement and apply it to player rigidbody component:
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -_moveData.x * MovementRate * Time.deltaTime));
            
            // Rotate player based on movement:
            transform.Rotate(Vector3.up * _moveData.x * 50f * Time.deltaTime);

            if (_moveData != Vector2.zero) return;
            var rotation = transform.rotation;
            rotation = Quaternion.Lerp(Quaternion.Euler(
                    rotation.eulerAngles.x,
                    rotation.eulerAngles.y,
                    rotation.eulerAngles.z),
                OriginalRot, 
                Time.deltaTime);
                
            transform.rotation = rotation;
        }

        private void OnEnable() {
            _controlsScript.Player.Enable();
        }

        private void OnDisable() {
            _controlsScript.Player.Disable();
        }
    }
}

