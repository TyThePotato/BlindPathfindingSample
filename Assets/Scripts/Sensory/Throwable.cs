using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Effects;
using UnityEngine;

namespace PathfindingGame.Sensory {

    public class Throwable : MonoBehaviour {
        
        private void Update() {
            // Spin through air
            transform.Rotate(Vector3.one * (180f * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision other) {
            
            // Create impact sound
            SensoryHelper.PlayImpactSound(transform.position);
            
            // Destroy throwable
            EffectsHelper.CreateDestroyEffect(transform.position);
            Destroy(gameObject);
        }

    }

}