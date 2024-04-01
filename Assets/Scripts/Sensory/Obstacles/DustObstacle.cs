using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Sensory.Obstacles {

    public class DustObstacle : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            // make player sneeze/cough
            if (other.CompareTag("Player"))
                SensoryHelper.PlayPlayerSound(other.transform.position);
        }

    }

}