// Brackeys - https://www.youtube.com/watch?v=MFQhpwc6cKE&t=367s&ab_channel=Brackeys - 2017

using System;
using UnityEngine;

namespace Camera {
    public class CameraFollow : MonoBehaviour {
        public Transform target;
        private const float Smoothing = 0.001f;
        private static readonly Vector3 Offset = new Vector3(-55f, 23f, 0f);

        private void LateUpdate() {
            var desiredPosition = target.position + Offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Smoothing);
            transform.position = smoothedPosition;
        }
    }
}
