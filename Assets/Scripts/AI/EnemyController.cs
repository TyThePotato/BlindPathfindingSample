using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PathfindingGame.AI {

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour {

        public float hearingStrength = 20.0f;
        public float smellingStrength = 5.0f;

        private NavMeshAgent _agent;
        private Vector3 _target;

        private void Awake() {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SetTarget(Vector3 target) {
            _target = target;
            
            Debug.Log("New enemy target");
        }

        public bool CanHear(Vector3 location, float volume) {
            var maxSqrDist = hearingStrength * hearingStrength; // sqr distance is cheaper than distance
            maxSqrDist *= volume * volume; // square volume to keep proportions

            var sqrDist = (transform.position - location).sqrMagnitude;
            return sqrDist <= maxSqrDist;
        }
        
    }

}