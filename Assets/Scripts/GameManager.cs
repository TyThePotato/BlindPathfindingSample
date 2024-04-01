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
        private GameObject _player;
        
        private GameObject _camera;
        private FollowCamera _cameraScript;

        private GameObject _enemy;
        private EnemyController _enemyController;

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

    }

}