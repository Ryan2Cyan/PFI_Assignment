using System;
using Asteroids;
using UnityEngine;

namespace Player {
    public class LaserAbility : MonoBehaviour {
        public Transform player;
        private float _offset;

        private void Awake() {
            _offset = 102f;
        }

        private void LateUpdate() {
            transform.position = new Vector3(player.position.x + _offset, player.position.y, player.position.z);
        }
        
        private void OnTriggerEnter(Collider target) {
            if (target.gameObject.CompareTag("Asteroid")) {
                Debug.Log("Asteroid Collision");
                target.GetComponent<Asteroid>().DamageAsteroid(100);
            }
        }
    }
}
