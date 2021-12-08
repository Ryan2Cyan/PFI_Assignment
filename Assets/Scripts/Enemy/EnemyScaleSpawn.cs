﻿// Rory Clark - https://rory.games - 2019

using Asteroids;
using UnityEngine;

// Small utility script that will scale the object from 0 to desired over time
// Once the scale is reached it will then remove itself (only the script) from the object to prevent it using any further performance
namespace Enemy {
    public class EnemyScaleSpawn : MonoBehaviour
    {
        [SerializeField]
        private float desiredScale; 
        private float _lerpTime = 1f;
        private float _time = 0; 
        private float _currentScale = 0f;

        private void Awake() {
            
            // Set asteroid scale to (0,0,0):
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            // m_time += Time.deltaTime;
            //
            // if(m_time > m_lerpTime)
            // {
            //     m_time = m_lerpTime;
            // }
            //
            // // Lerp between 0 and 2:
            // m_currentScale = Mathf.Lerp(0, m_desiredScale, m_time/m_lerpTime);
            //
            // // Set currentScale to the localScale:
            // transform.localScale = new Vector3(m_currentScale, m_currentScale, m_currentScale);
            //
            // if(m_time >= m_lerpTime)
            // {
            //     Destroy(this);
            // }
        }
    }
}
