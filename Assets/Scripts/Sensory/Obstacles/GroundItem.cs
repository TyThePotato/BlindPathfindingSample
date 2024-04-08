using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Input;
using PathfindingGame.Player;
using UnityEngine;

namespace PathfindingGame.Sensory.Obstacles {

    public class GroundItem : MonoBehaviour {

        public byte itemID;
        
        private bool _isPlayerWithinPickupRange;
        private bool _isPlayerSteppingOn;

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            
            if (_isPlayerWithinPickupRange) {
                // entered inner trigger
                Debug.Log("Entered inner trigger");
                
                SensoryHelper.PlayImpactSound(transform.position);
                _isPlayerSteppingOn = true;
            }
            else {
                // entered outer trigger
                Debug.Log("Entered outer trigger");
                
                other.GetComponent<PlayerThrowables>().SetGrabTarget(this);
                _isPlayerWithinPickupRange = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player")) return;

            if (_isPlayerSteppingOn) {
                // exited inner trigger
                Debug.Log("Exited inner trigger");
                _isPlayerSteppingOn = false;
            }
            else {
                // exited outer trigger
                Debug.Log("Exited outer trigger");
                
                other.GetComponent<PlayerThrowables>().ClearGrabTarget(this);
                _isPlayerWithinPickupRange = false;
            }
        }

    }

}