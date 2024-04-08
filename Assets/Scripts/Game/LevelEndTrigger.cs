using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathfindingGame.Game {

    public class LevelEndTrigger : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            
            if (GameManager.HasKey)
                GameManager.FinishLevel();
        }

    }

}