using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Sensory {

    public class Throwable : MonoBehaviour {

        private void OnCollisionEnter(Collision other) {
            
            // Create impact sound
            SensoryHelper.PlayImpactSound(transform.position);
            
            // Destroy throwable
            // TODO: Explosion Particles
            Destroy(this);
        }

    }

}