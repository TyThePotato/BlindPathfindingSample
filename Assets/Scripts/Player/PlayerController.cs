using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingGame.Input;
using PathfindingGame.Sensory;

namespace PathfindingGame.Player {

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {

        public float speed = 4.0f;
        public float speedEasing = 4.0f;
        public float turnSmoothing = 3.0f;

        public bool isSneaking = false;
        public float sneakingMultiplier = 0.5f;
        
        private CharacterController _controller;

        public Vector3 TargetVelocity => _velocity;
        public Vector3 RealVelocity => _realVelocity;
        
        private Vector3 _velocity; // current charactercontroller velocity
        private Vector3 _realVelocity; // real gameobject velocity
        private Vector3 _lastPos; // position last frame; used to calculate real velocity
        private float _footstepProgress;
        
        private void Start() {

            _controller = GetComponent<CharacterController>();
            
            // bind actions
            InputHelper.SneakAction.started += _ => BeginSneaking();
            InputHelper.SneakAction.canceled += _ => EndSneaking();
        }

        private void Update() {
            _realVelocity = (transform.position - _lastPos) / Time.deltaTime;
            _lastPos = transform.position;
            
            DoMovement();
        }

        private void DoMovement() {
            // calculate target velocity
            var inputVector = InputHelper.Movement;
            var targetVelocity = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
            targetVelocity *= speed;

            if (isSneaking)
                targetVelocity *= sneakingMultiplier;
            
            // ease velocity
            // keep lerp amount <= 1 to avoid overshooting at low framerates
            var smooth = speedEasing * Time.deltaTime;
            if (smooth > 1.0f)
                smooth = 1.0f;

            _velocity = Vector3.Lerp(_velocity, targetVelocity, smooth);
            
            // gravity
            _velocity.y = -9f; // TODO: easing

            _controller.Move(_velocity * Time.deltaTime);

            // face movement direction
            if (targetVelocity != Vector3.zero) {
                var dir = Quaternion.LookRotation(targetVelocity);

                // keep lerp amount <= 1 to avoid overshooting at low framerates
                smooth = turnSmoothing * Time.deltaTime;
                if (smooth > 1.0f)
                    smooth = 1.0f;
                
                transform.rotation = Quaternion.Slerp(transform.rotation, dir, smooth);
            }
        }
        
        private void BeginSneaking() {
            isSneaking = true;
        }

        private void EndSneaking() {
            isSneaking = false;
        }

    }

}