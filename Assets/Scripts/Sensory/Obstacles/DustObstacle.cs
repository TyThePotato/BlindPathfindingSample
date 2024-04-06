using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Player;
using UnityEngine;

namespace PathfindingGame.Sensory.Obstacles {

    public class DustObstacle : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            // make player sneeze/cough
            if (other.CompareTag("Player")) {
                // cough/sneeze
                SensoryHelper.PlayPlayerSound(other.transform.position);

                // make player stink
                other.GetComponent<PlayerSensory>().smellStrength = 2.0f;
            }
        }

    }

}