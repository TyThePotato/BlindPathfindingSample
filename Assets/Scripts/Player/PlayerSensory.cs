using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingGame.Input;
using PathfindingGame.Sensory;

namespace PathfindingGame.Player { // PathfindingGame.Sensory?

    public class PlayerSensory : MonoBehaviour {

        public float footstepThreshold = 0.25f;
        public float smellCrumbThreshold = 1.0f;

        public bool isSneaking = false;
        public float smellStrength = 1.0f;
        
        private PlayerController _controller;
        private float _footstepProgress;
        private float _smellCrumbProgress;

        void Start() {
            _controller = GetComponent<PlayerController>();
            
            // bind actions
            InputHelper.SneakAction.started += _ => BeginSneaking();
            InputHelper.SneakAction.canceled += _ => EndSneaking();
        }

        void Update() {
            DoFootsteps();
            DoSmell();
        }

        void DoFootsteps() {
            // dont create footsteps if player is not moving or trying to move
            if (_controller.RealVelocity == Vector3.zero || _controller.TargetVelocity == Vector3.zero)
                return;
            
            
            _footstepProgress += Time.deltaTime * _controller.RealVelocity.sqrMagnitude 
                                 / (_controller.speed * _controller.speed);
            
            if (_footstepProgress >= footstepThreshold) {
                
                // determine footstep sound properties
                var stepType = SensoryHelper.GetFootstepMaterial(transform.position, Vector3.down);
                var strength = isSneaking ? 0.25f : 1.0f; // sound effect strength
                SensoryHelper.PlayFootstepSound(stepType, transform.position, strength);
                
                _footstepProgress = 0.0f; // reset
            }
        }

        void DoSmell() {
            _smellCrumbProgress += Time.deltaTime;
            if (_smellCrumbProgress >= smellCrumbThreshold) {
                
                // create new smell crumb
                SensoryHelper.CreateSmellCrumb(transform.position, smellStrength);
                
                _smellCrumbProgress = 0.0f; // reset
            }
        }

        void BeginSneaking() {
            isSneaking = true;
        }

        void EndSneaking() {
            isSneaking = false;
        }
        
    }

}
