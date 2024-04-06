using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingGame.Input;

namespace PathfindingGame.Player {

    public class PlayerThrowables : MonoBehaviour {

        public GameObject[] throwablePrefabs;
        public float throwStrength = 10.0f;
        public float throwHeight = 3.0f;

        public bool hasThrowable;
        public byte currentThrowable;
        
        void Start() {
            // register input event
            InputHelper.ThrowAction.performed += _ => ThrowObject();
        }

        void ThrowObject() {
            if (!hasThrowable)
                return;
            
            Debug.Log("Throwing!");

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