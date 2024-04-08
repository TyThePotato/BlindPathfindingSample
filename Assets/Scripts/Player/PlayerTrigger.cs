using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingGame.Game;

namespace PathfindingGame.Player {

    public class PlayerTrigger : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Enemy")) {
                // END GAME
                GameManager.CatchPlayer();
            }
        }

    }

}