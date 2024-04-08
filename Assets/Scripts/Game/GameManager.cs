using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.AI;
using PathfindingGame.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathfindingGame.Game {

    public class GameManager : MonoBehaviour {

        // Spawnpoints
        public Transform playerSpawnpoint;
        public Transform enemySpawnpoint;
        
        // Prefabs
        public GameObject playerPrefab;
        public GameObject cameraPrefab;
        public GameObject enemyPrefab;
        
        // Game State
        public static bool HasKey = false; // does this really need to be static
        
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
            
            // Reset haskey (static variables dont get automatically reset)
            HasKey = false;
        }

        public static void SetEnemyTarget(Vector3 position) {
            Instance._enemyController.SetTarget(position, true);
        }

        public static void IncreaseEnemySensitivity(float multiplier) {
            Instance._enemyController.hearingStrength *= multiplier;
            Instance._enemyController.smellingStrength *= multiplier;
        }

        public static void FinishLevel() {
            Time.timeScale = 0f;
            UIManager.ShowWinScreen();
        }
        
        public static void CatchPlayer() {
            Time.timeScale = 0f;
            UIManager.ShowFailScreen();
        }

        public static void Restart() {
            Time.timeScale = 1f;

            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            
            // this is so broken for some reason and i do not have the will to troubleshoot it any longer
            // there is a random chance (!) that the player/enemy does not get positioned correctly
            // when the scene reloads. sometimes it works correctly. why?
        }

    }

}