using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Sensory;
using UnityEngine;
using UnityEngine.AI;

namespace PathfindingGame.AI {

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour {

        public float hearingStrength = 20.0f;
        public float smellingStrength = 5.0f;

        private NavMeshAgent _agent;
        private Vector3 _target;

        private float _currentTargetTimestamp = 0.0f;

        private AudioSource _alertAudioSource;

        private AIState _state;

        void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _alertAudioSource = GetComponent<AudioSource>();
            
            // register sensory events
            SensoryHelper.NewSound.AddListener(NewSound);
            
            // set debug values for sensory visualization
            SensoryHelper.AudioVisualizationRange = hearingStrength * 2;
            SensoryHelper.SmellVisualizationRange = smellingStrength * 2;
        }

        void Update() {
            // TODO: poll agent destination distance, stop and change state when close
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
        /// Starts navigating towards specified point
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isSound"></param>
        void SetTarget(Vector3 target, bool isSound) {
            _target = target;

            // play alert sound if not already traveling
            if (!_agent.hasPath)
                _alertAudioSource.Play();
            
            // begin traveling to new target
            _agent.destination = _target;
            
            // set state
            _state = isSound ? AIState.TrackingSound : AIState.TrackingSmell;
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

        enum AIState {

            Idle,
            TrackingSound,
            TrackingSmell

        }

    }

}