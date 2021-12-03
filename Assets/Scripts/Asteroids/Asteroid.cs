using UnityEngine;

namespace Asteroids {
    public class Asteroid {
        private float Health { get; }
        private Vector3 Scale { get; }
        private float Velocity { get; }
    
        // Constructor:
        public Asteroid(float health, Vector3 scale, float velocity) {
            Health = health;
            Scale = scale;
            Velocity = velocity;
        }
    }
}
