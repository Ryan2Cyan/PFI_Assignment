using System;
using System.Diagnostics;
using UnityEngine;

namespace Asteroids {
    public class Asteroid : MonoBehaviour {
        private float Health { get; }
        private Vector3 Scale { get; }
        private float Velocity { get; }
        private float ScoreValue { get; }
        private AsteroidType AsteroidType { get; }
    
        // Constructor:
        public Asteroid(AsteroidType asteroidType) {
            // Here we need a way to assign all the asteroids properties on initialisation
            // based on an enum input param.
            switch (asteroidType) {
                case AsteroidType.A1:
                    Health = 2f;
                    break;
                case AsteroidType.A2:
                    break;
                case AsteroidType.A3:
                    break;
                case AsteroidType.A4:
                    break;
                case AsteroidType.A5:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(asteroidType), asteroidType, null);
            }
        }

        private void Awake() {
            // Enemy Scale Spawn Function
        }
    }

    public enum AsteroidType {
        A1,
        A2,
        A3,
        A4,
        A5
    };
    
}
