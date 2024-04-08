using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingGame.Input;
using PathfindingGame.Sensory.Obstacles;

namespace PathfindingGame.Player {

    public class PlayerThrowables : MonoBehaviour {

        public GameObject[] throwablePrefabs;
        public float throwStrength = 10.0f;
        public float throwHeight = 3.0f;

        public bool hasThrowable;
        public byte currentThrowable;

        private GroundItem _targetItem;
        
        void Start() {
            // register input event
            InputHelper.ThrowAction.performed += _ => ItemButtonPressed();
        }

        public void SetGrabTarget(GroundItem target) {
            if (hasThrowable) return;
            _targetItem = target;
            
            UIManager.SetItemPickupText(target.itemID);
        }

        public void ClearGrabTarget(GroundItem self) {
            if (hasThrowable) return;
            
            if (_targetItem == self) {
                _targetItem = null;
                UIManager.SetNoCurrentItem();
            }
        }

        void ItemButtonPressed() {
            if (hasThrowable) {
                ThrowObject();
            } else if (_targetItem != null) {
                GrabObject();
            }
        }

        void GrabObject() {
            if (_targetItem == null)
                return;

            UIManager.SetCurrentItemText(_targetItem.itemID);

            currentThrowable = _targetItem.itemID;
            hasThrowable = true;

            Destroy(_targetItem.gameObject);
            _targetItem = null;
        }

        void ThrowObject() {
            if (!hasThrowable)
                return;
            
            UIManager.SetNoCurrentItem();

            // TODO: error checking/protection against invalid index
            var go = Instantiate(throwablePrefabs[currentThrowable]);
            go.transform.position = transform.position + transform.forward + Vector3.up;
            
            // apply force
            go.GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength + 
                                                  Vector3.up * throwHeight);

            hasThrowable = false;
        }

    }

}