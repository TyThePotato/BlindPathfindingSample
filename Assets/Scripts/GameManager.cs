using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.AI;
using PathfindingGame.Player;
using UnityEngine;

namespace PathfindingGame {

    public class GameManager : MonoBehaviour {

        // Spawnpoints
        public Transform playerSpawnpoint;
        public Transform enemySpawnpoint;
        
        // Prefabs
        public GameObject playerPrefab;
        public GameObject cameraPrefab;
        public GameObject enemyPrefab;
        
        // References
        public static GameManager Instance;
        
        private GameObject _player;
        
        private GameObject _camera;
        private FollowCamera _cameraScript;

        private GameObject _enemy;
        private EnemyController _enemyController;

        void Awake() {
            Instance = this;
        }

        void Start() {
            // spawn prefabs
            _player = Instantiate(playerPrefab);
            _player.transform.position = playerSpawnpoint.position;

            _camera = Instantiate(cameraPrefab);
            _cameraScript = _camera.GetComponent<FollowCamera>();

            _enemy = Instantiate(enemyPrefab);
            _enemy.transform.position = enemySpawnpoint.position;
            _enemyController = _enemy.GetComponent<EnemyController>();
            
            // Focus player
            _cameraScript.SetTarget(_player.transform);
        }

        public void EndGame() {
            Debug.Log("GAME END");
            
            // pause editor
            Debug.Break();
        }

    }

}