using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

namespace Core {
    public class PauseScreen : MonoBehaviour {

        private          bool     _isPaused      = false;
        private readonly string[] _noPauseScenes = {"MainMenu"};

        private void Start() { ChangeActiveStateTo(false); }


        private void Update() {
            if ( Input.GetButtonDown("Cancel") ) { TogglePause(); }
        }


        public bool IsPaused() { return _isPaused; }


        /// <summary>
        /// Toggle pause menu state and trigger the relative mecanism
        /// </summary>
        private void TogglePause() {
            if ( !CheckCanPause() ) return;

            _isPaused = !_isPaused;
            ChangeActiveStateTo(_isPaused);

            Time.timeScale = _isPaused ? 0f : 1f;
        }


        public void SetTimeScale(float scale) { Time.timeScale = scale; }


        /// <summary>
        /// Pause state setter
        /// </summary>
        /// <param name="state"></param>
        public void SetStateTo(bool state) {
            _isPaused = state;
            ChangeActiveStateTo(state);

            Time.timeScale = state ? 0f : 1f;
        }


        /// <summary>
        /// Check if the user can currently pause the game, relative to the scene
        /// </summary>
        /// <returns></returns>
        private bool CheckCanPause() {
            string name = SceneManager.GetActiveScene().name;
            return !_noPauseScenes.Contains(name);
        }


        /// <summary>
        /// Change the active state of child elements
        /// </summary>
        /// <param name="state"></param>
        private void ChangeActiveStateTo(bool state) {
            foreach ( Transform child in transform ) { child.gameObject.SetActive(state); }
        }

    }
}