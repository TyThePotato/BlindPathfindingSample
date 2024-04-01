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

        private AudioSource _alertAudioSource;

        private void Awake() {
            _agent = GetComponent<NavMeshAgent>();
            _alertAudioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            // register sensory events
            SensoryHelper.NewSound.AddListener(NewSound);
            
            // set debug values for sensory visualization
            SensoryHelper.AudioVisualizationRange = hearingStrength * 2;
            SensoryHelper.SmellVisualizationRange = smellingStrength * 2;
        }

        private void NewSound(Vector3 position, float strength) {
            if (!CanHear(position, strength))
                return;
            
            SetTarget(position);
        }

        public void SetTarget(Vector3 target) {
            _target = target;

            // play alert sound if not already traveling
            if (!_agent.hasPath)
                _alertAudioSource.Play();
            
            // begin traveling to new target
            _agent.destination = _target;
        }

        public bool CanHear(Vector3 location, float volume) {
            var maxSqrDist = hearingStrength * hearingStrength; // sqr distance is cheaper than distance
            maxSqrDist *= volume * volume; // square volume to keep proportions

            var sqrDist = (transform.position - location).sqrMagnitude;
            return sqrDist <= maxSqrDist;
        }
        
    }

}