using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Sensory;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// TODO: create base movement class that playercontroller and enemycontroller inherit from

namespace PathfindingGame.AI {

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterController))]
    public class EnemyController : MonoBehaviour {

        public float speedEasing = 4.0f;
        public float turnSmoothing = 3.0f;
        
        public float hearingStrength = 20.0f;
        public float smellingStrength = 5.0f;

        public float runSpeed = 5.0f;
        public float wanderSpeed = 3.0f;
        public float wanderRange = 8.0f;

        private NavMeshAgent _agent;
        private CharacterController _controller;
        
        private Vector3 _velocity;
        
        private Vector3 _target;
        private float _currentTargetTimestamp = 0.0f;

        private AIState _state = AIState.Wander;

        void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _controller = GetComponent<CharacterController>();
            
            // register sensory events
            SensoryHelper.NewSound.AddListener(NewSound);
            
            // set initial speed
            _agent.speed = runSpeed;
            
            // set debug values for sensory visualization
            UpdateVisualizationValues();
        }

        void Update() {
            // lock agent velocity to prevent automatic navigation
            // there must be a proper way to do this but i am currently without internet connection
            _agent.velocity = Vector3.zero;
            
            DoMovement();
            _controller.Move(_agent.desiredVelocity * Time.deltaTime);
        }
        
        void OnTriggerEnter(Collider other) {
            
            // Detect Smell Crumbs
            if (other.CompareTag("SmellCrumb")) {

                // Check if smell is older than the current sensory target
                if (other.GetComponent<SmellCrumbInfo>().Timestamp < _currentTargetTimestamp) {
                    return;
                }
                
                SetTarget(other.transform.position, false);
            }
        }

        void DoMovement() {
            // TODO: poll agent destination distance, stop and change state when close
            var targetVel = _agent.desiredVelocity;
            
            // ease velocity
            // keep lerp amount <= 1 to avoid overshooting at low framerates
            var smooth = speedEasing * Time.deltaTime;
            if (smooth > 1.0f)
                smooth = 1.0f;
            
            _velocity = Vector3.Lerp(_velocity, targetVel, smooth);
            
            // add gravity
            _velocity.y = -9f; // TODO: easing

            _controller.Move(_velocity * Time.deltaTime);
            
            // face movement direction
            if (targetVel != Vector3.zero) {
                var dir = Quaternion.LookRotation(targetVel);

                // keep lerp amount <= 1 to avoid overshooting at low framerates
                smooth = turnSmoothing * Time.deltaTime;
                if (smooth > 1.0f)
                    smooth = 1.0f;
                
                transform.rotation = Quaternion.Slerp(transform.rotation, dir, smooth);
            }
            
            // Destination reach check
            if (_agent.remainingDistance < 0.25f) {
                // wander once destination reached
                SetWanderTarget();
            }
        }
        
        /// <summary>
        /// Starts navigating towards specified point
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isSound"></param>
        public void SetTarget(Vector3 target, bool isSound) {
            _target = target;
            _agent.SetDestination(target);
            _agent.speed = runSpeed;
            
            // set state
            _state = isSound ? AIState.TrackingSound : AIState.TrackingSmell;

            Debug.Log("New Enemy Target!");
        }

        void SetWanderTarget() {
            // randomize coordinates
            var x = Random.Range(-wanderRange, wanderRange);
            var z = Random.Range(-wanderRange, wanderRange);
            
            // keep away from self
            if (x is > 0f and < 1.0f)
                x = 1f;
            else if (x is < 0f and > -1.0f)
                x = -1f;

            if (z is > 0f and < 1.0f)
                z = 1f;
            else if (z is < 0f and > -1.0f)
                z = -1f;
            
            // set target
            var target = new Vector3(x, 0f, z);
            _target = target;
            _agent.SetDestination(target);
            _agent.speed = wanderSpeed;

            _state = AIState.Wander;
        }

        /// <summary>
        /// Sound effect callback - called when a new sound effect is played in the scene
        /// </summary>
        /// <param name="position"></param>
        /// <param name="strength"></param>
        void NewSound(Vector3 position, float strength) {
            if (!CanHear(position, strength))
                return;

            _currentTargetTimestamp = Time.time;
            
            SetTarget(position, true);
        }

        /// <summary>
        /// Returns whether a sound with the specified parameters is audible to the enemy
        /// </summary>
        /// <param name="location"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        bool CanHear(Vector3 location, float volume) {
            var maxSqrDist = hearingStrength * hearingStrength; // sqr distance is cheaper than distance
            maxSqrDist *= volume * volume; // square volume to keep proportions

            var sqrDist = (transform.position - location).sqrMagnitude;
            return sqrDist <= maxSqrDist;
        }

        enum AIState {

            Wander,
            TrackingSound,
            TrackingSmell

        }

        // DEBUG / VISUALIZATION
        public void UpdateVisualizationValues() {
            SensoryHelper.AudioVisualizationRange = hearingStrength * 2;
            SensoryHelper.SmellVisualizationRange = smellingStrength * 2;
        }

    }

}