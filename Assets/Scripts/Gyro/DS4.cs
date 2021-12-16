// Code by SG4YK: 
// https://blog.sg4yk.com/dual_shock_motion_in_unity_en.html - 2020

using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Gyro {
    public class DS4 {
        
        // Gyroscope:
        public static ButtonControl gyroX = null;
        public static ButtonControl gyroY = null;
        public static ButtonControl gyroZ = null;
        // Controller:
        public static Gamepad Controller = null;
        
        // Func that replaces default layout of DS4 with one defined in JSON file:
        public static Gamepad GetController(string layoutFile = null) {
            
            // Read layout from JSON file:
            var layout =
                File.ReadAllText(layoutFile ?? "Assets/Scripts/JSON/DS4_Custom_Layout.json");
            
            // Overwrite the default layout:
            InputSystem.RegisterLayoutOverride(layout, "DualShock4GamepadHID");
            
            // Return overridden gamepad:
            var ds4 = Gamepad.current;
            Controller = ds4;
            BindControls(Controller);
            return Controller;
        }
        
        // Binds gyro controls to controller:
        private static void BindControls(InputControl ds4) {
            gyroX = ds4.GetChildControl<ButtonControl>("gyro X 14");
            gyroY = ds4.GetChildControl<ButtonControl>("gyro Y 16");
            gyroZ = ds4.GetChildControl<ButtonControl>("gyro Z 18");
        }
        
        // Retrieve rotation data:

        public static Quaternion GetRotation(float scale = 1) {
            // var x = ProcessRawData(gyroX.ReadValue()) * scale;
            // var y = ProcessRawData(gyroY.ReadValue()) * scale;
            var z = ProcessRawData(gyroZ.ReadValue()) * scale;
            return Quaternion.Euler(0f, 0f, z);
        }
        
        // Convert data from gyro:
        public static float ProcessRawData(float data) {
            if (data < 1 && data > 0) {
                if (data > 0.5) {
                    Debug.Log("Right");
                }
                else {
                    Debug.Log("Left");
                }
            }
            return data > 0.5 ? 1 - data : -data;
            
        } 
    }
}
