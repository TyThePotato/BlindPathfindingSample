using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Effects {

    public class EffectsHelper : MonoBehaviour {

        private static EffectsHelper _instance;

        public GameObject destroyEffectPrefab;
        
        private void Awake() {
            _instance = this;
        }

        public static void CreateDestroyEffect(Vector3 location) {
            var go = Instantiate(_instance.destroyEffectPrefab);
            go.transform.position = location;

            Destroy(go, 1.0f);
        }

    }

}