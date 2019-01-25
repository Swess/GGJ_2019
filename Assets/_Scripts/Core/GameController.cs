using System;
using Core.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class GameController : MonoBehaviour {

        private static GameController _instance; // Singleton instance

        public static GameController Instance {
            get { return _instance; }
        }


        private bool _testingMode = false; // Define if we are testing a scene alone or using the SceneController Loader

        /// References
        private GameObject _player1;
        private GameObject _player2;

        [SerializeField] private SceneController _sceneController;


        // ====================================
        // ====================================

        /// <summary>
        /// Accessors for different important Components
        /// </summary>

        public GameObject Player1 {
            get {
                if ( _player1 == null ) _player1 = GameObject.Find("Player1");
                return _player1;
            }
        }

        public GameObject Player2 {
            get {
                if ( _player2 == null ) _player2 = GameObject.Find("Player2");
                return _player2;
            }
        }

        public SceneController SceneController {
            get { return _sceneController; }
        }


        public ActionsMapsHelper actionsMapsHelper { get; protected set; }

        // ====================================
        // ====================================


        /// <summary>
        /// Brute constructor.
        /// Called even before Awake() calls.
        /// </summary>
        public GameController() {
            // Setup Singleton
            if ( _instance == null )
                _instance = this;
            else if ( _instance != this ) Destroy(gameObject);
        }


        private void Awake() {
            // Define if we are testing a scene
            if ( SceneManager.GetActiveScene().name != "Persistent" ) {
                _testingMode = true;
            }

            actionsMapsHelper = new ActionsMapsHelper();
        }


        // ========================================================
        // ========================================================
        // ========================================================


        public bool IsTesting() { return _testingMode; }


        /// <summary>
        /// Quit the entire application (Only in builds)
        /// </summary>
        public void KillGame() { Application.Quit(); }

    } // Class
}