using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Sensory {

    public class SmellCrumbInfo : MonoBehaviour {
        
        public float Timestamp => _timestamp;
        private float _timestamp;

        private void Start() {
            _timestamp = Time.time;
        }

    }

}