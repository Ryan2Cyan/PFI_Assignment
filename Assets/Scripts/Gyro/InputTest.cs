// Code by SG4YK: 
// https://blog.sg4yk.com/dual_shock_motion_in_unity_en.html - 2020

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gyro {
    public class InputTest : MonoBehaviour {
        
        private Gamepad _controller;
        private Transform _transform;

        private void Start() {
            _controller = DS4.GetController();
            _transform = transform;
        }

        private void Update() {
            // if (_controller == null) {
            //     try {
            //         _controller = DS4.GetController();
            //     }
            //     catch (Exception e) {
            //         Console.WriteLine(e);
            //     }
            // }
            // else {
            //     // Press circle button to reset rotation:
            //     if (_controller.buttonEast.IsPressed()) {
            //         _transform.rotation = Quaternion.identity;
            //     }
            //
            //     _transform.rotation *= DS4.GetRotation(4000 * Time.deltaTime);
            //     print(DS4.GetRotation(4000 * Time.deltaTime));
            // }
        }
    }
}
