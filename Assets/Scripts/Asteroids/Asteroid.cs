using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids {
    public class Asteroid : MonoBehaviour{

        [SerializeField] private float health;
        public float Health => health;
        
        [SerializeField] private float velocity;
        public float Velocity => velocity;
        
        [SerializeField] private float spawnRate;
        public float SpawnRate => spawnRate;

        [SerializeField] private Vector3 scale;
        public Vector3 Scale => scale;

        [SerializeField] private int scoreValue;
        public int ScoreValue => scoreValue;

        [SerializeField] private AsteroidType asteroidType;
        public AsteroidType AsteroidType => asteroidType;

        private Rigidbody _asteroidRigidbody;
        [SerializeField]
        private float desiredScale;
        private float _time = 0; 
        private float _currentScale = 0f;


        // Constructor
        public Asteroid(AsteroidType asteroidTypeParam) {
            asteroidType = asteroidTypeParam;
        }
        
        private void Awake() {
            
            // Assign asteroid type according to spawn rate:
            var randNumb = Random.Range(0, 100);
            if (randNumb < 10) {
                asteroidType = AsteroidType.A1;
            }
            else if (randNumb >= 10 && randNumb < 35) {
                asteroidType = AsteroidType.A2;
            }
            else if (randNumb >= 35 && randNumb < 75) {
                asteroidType = AsteroidType.A3;
            }
            else if (randNumb >= 75 && randNumb < 90) {
                asteroidType = AsteroidType.A4;
            }
            else if (randNumb >= 90 && randNumb < 101) {
                asteroidType = AsteroidType.A5;
            }
            
            // Assign all the asteroids properties on initialisation based on an enum input param.
            switch (asteroidType) {
                case AsteroidType.A1:
                    asteroidType = AsteroidType.A1;
                    health = 2f;
                    velocity = 35f;
                    spawnRate = 20f;
                    scale = new Vector3(1f, 1f, 1f);
                    scoreValue = 50;
                    break;
                case AsteroidType.A2:
                    asteroidType = AsteroidType.A2;
                    health = 6f;
                    velocity = 30f;
                    spawnRate = 25f;
                    scale = new Vector3(1.5f, 1.5f, 1.5f);
                    scoreValue = 30;
                    break;
                case AsteroidType.A3:
                    asteroidType = AsteroidType.A3;
                    health = 10f;
                    velocity = 25f;
                    spawnRate = 30f;
                    scale = new Vector3(2f, 2f, 2f);
                    scoreValue = 30;
                    break;
                case AsteroidType.A4:
                    asteroidType = AsteroidType.A4;
                    health = 15f;
                    velocity = 20f;
                    spawnRate = 15f;
                    scale = new Vector3(2.5f, 2.5f, 2.5f);
                    scoreValue = 40;
                    break;
                case AsteroidType.A5:
                    asteroidType = AsteroidType.A5;
                    health = 20f;
                    velocity = 5f;
                    spawnRate = 10f;
                    scale = new Vector3(3f, 3f, 3f);
                    scoreValue = 50;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(asteroidType), asteroidType, null);
            }
            
            // Set asteroid scale to (0,0,0):
            transform.localScale = Vector3.zero;
            
            // Add torque:
            _asteroidRigidbody = GetComponent<Rigidbody>();
            AddRandomTorque(_asteroidRigidbody);
        }

        private void Update() {
            
            ClampVelocity(Velocity, _asteroidRigidbody);
            OutOfBoundsCheck(transform.position.x, -20f);
            
            // Scale on spawn from (0,0,0) to the set scale:
            _time += Time.deltaTime;
            var _lerpTime = 1f;
            
            if(_time > _lerpTime)
            {
                _time = _lerpTime;
            }
            
            // Lerp between 0 and 2:
            _currentScale = Mathf.Lerp(0, Scale.x, _time/_lerpTime);
            print("lerp_Time: " + _lerpTime);
            print("Time: " + _time);
            print("Scale.x :" + Scale.x);
            print("current scale: " + _currentScale);
            // Set currentScale to the localScale:
            transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
            
            // if(_time >= _lerpTime)
            // {
            //     Destroy(this);
            // }
        }

        private static void AddRandomTorque(Rigidbody rigidbody) {
            const float minRandomTorque = -10; 
            const float maxRandomTorque = 10;
            rigidbody.AddRelativeTorque(new Vector3(Random.Range(minRandomTorque, maxRandomTorque), 
                Random.Range(minRandomTorque, maxRandomTorque), Random.Range(minRandomTorque, 
                    maxRandomTorque)));
        }
        private static void ClampVelocity(float maxVelocity, Rigidbody rigidbody) {
            if (rigidbody.velocity.magnitude > maxVelocity) {
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
            }
        }
        private void OutOfBoundsCheck(float xPos, float threshold) {
            if (xPos <= threshold)
                Destroy(gameObject);
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
