using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Player {

    public class FollowCamera : MonoBehaviour {

        public Transform target;
        
        public float distance = 5.0f;
        public float height = 3.0f;

        public float smoothingAmount = 1.0f;
        
        private bool _targetSet = false;

        private void Awake() {
            _targetSet = target != null;
        }

        public void SetTarget(Transform newTarget) {
            target = newTarget;
            _targetSet = true;
        }

        private void Update() {
            if (_targetSet)
                TrackTarget();
        }

        private void TrackTarget() {
            // calculate target camera position
            var targetPosition = target.position;
            targetPosition += Vector3.up * height;
            targetPosition += Vector3.back * distance;

            // look at target
            transform.LookAt(target);

            // keep lerp amount <= 1 to avoid overshooting at low framerates
            var smooth = smoothingAmount * Time.deltaTime;
            if (smooth > 1.0f)
                smooth = 1.0f;
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);
        }

    }

}