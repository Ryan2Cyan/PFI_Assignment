// Rory Clark - https://rory.games - 2019

using Asteroids;
using Camera;
using UnityEngine;

namespace Player {
    public class BulletDamage : MonoBehaviour
    {
        [SerializeField]
        float m_damage;

        private void OnTriggerEnter(Collider other)
        {
            // Damage the enemy if we hit one, destroy ourselves
            var enemy = other.GetComponent<Asteroid>();
            if (enemy != null)
            {
                enemy.DamageAsteroid(m_damage);
                Destroy(gameObject);
            }
        }
    }
}
