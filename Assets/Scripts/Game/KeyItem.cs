using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Sensory;
using UnityEngine;

namespace PathfindingGame.Game {

    public class KeyItem : MonoBehaviour {

        public Vector3 enemyTarget;
        public float enemySensitivityMultiplier = 1.5f;
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                SensoryHelper.PlayKeySound(transform.position);
                UIManager.SetObjective("Escape through the main gate");
                
                GameManager.HasKey = true;
                GameManager.SetEnemyTarget(enemyTarget);
                GameManager.IncreaseEnemySensitivity(enemySensitivityMultiplier);
                
                Destroy(gameObject);
            }
        }

    }

}