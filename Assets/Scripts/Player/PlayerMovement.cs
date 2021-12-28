// Rory Clark - https://rory.games - 2019

using System;
using Gyro;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {
        // Objects for movement:
        private static float _currentMovementRate;
        private const float BuffedMovementRate = 1500;
        private const float DefaultMovementRate = 700;
        private PFI_SpaceInvaders_Controller _controlsScript;
        private static Quaternion OriginalRot => Quaternion.Euler(
            55,
            90,
            0);
        
        private Vector2 _moveData;
        public Vector2 MoveData => _moveData;
        
        // Gyro Controls Variables:
        private Gamepad _controller;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private const float GyroShipRotMod = 5f;


        private void Awake() {
            
            // Set movement speed:
            _currentMovementRate = DefaultMovementRate;
            
            // Get ps4 controls:
            _controlsScript = new PFI_SpaceInvaders_Controller();
            if (Gamepad.current != null) {
                _controller = DS4.GetController();
            }

            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            
            // Link up data from controller to a variable (Movement):
            _controlsScript.Player.Move.performed += context => _moveData = context.ReadValue<Vector2>();
            _controlsScript.Player.Move.canceled += context => _moveData = context.ReadValue<Vector2>();
        }

        private void Update() {
            
            // Gyro Movement:
            // Check overridden control layer is initialised:
            if (_controller != null) {
                var gyroZData = DS4.ProcessRawData(DS4.gyroZ.ReadValue());
                if (_controller == null) {
                    try {
                        _controller = DS4.GetController();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                    }
                }
                else {
                    // Get data from gyro:
                    _transform.position += new Vector3(0f, 0f, -gyroZData * _currentMovementRate * Time.deltaTime);
                }
                transform.Rotate(Vector3.up * (gyroZData * 1000f * GyroShipRotMod * Time.deltaTime));
            }

            // Calculate new movement and apply it to player rigidbody component:
            _rigidbody.AddForce(new Vector3(0f, 0f, -_moveData.x * _currentMovementRate * Time.deltaTime));
            
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
            
        }

        private void OnEnable() {
            _controlsScript.Player.Enable();
        }

        private void OnDisable() {
            _controlsScript.Player.Disable();
        }

        public static void MovementSpeedBuff() {
            _currentMovementRate = BuffedMovementRate;
        }

        public static void ResetMovementSpeed() {
            _currentMovementRate = DefaultMovementRate;
        }
    }
}

