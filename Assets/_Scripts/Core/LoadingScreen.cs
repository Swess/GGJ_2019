using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Core {
    public class LoadingScreen : MonoBehaviour {

        public  GameObject progressBar;
        private float      loadProgress = 0; // From 0 to 100


        void Start() {
            // Adds delegates events function
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            ChangeStateTo(false);
        }


        /// <summary>
        /// Function triggering scene change and Async Loading
        /// </summary>
        /// <param name="sceneName"></param>
        public void ChangeToScene(string sceneName) { StartCoroutine(DisplayLoadingScreen(sceneName)); } //


        /// <summary>
        /// Async loading with pourcentage bar display
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        IEnumerator DisplayLoadingScreen(string sceneName) {
            // Setup GUI
            ChangeStateTo(true);

            progressBar.transform.localScale =
                new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

            while ( !async.isDone ) {
                loadProgress = async.progress;
                progressBar.transform.localScale =
                    new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

                yield return null;
            } // while
        }


        /// <summary>
        /// Delegate function on scene loaded
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="m"></param>
        void OnSceneLoaded(Scene sc, LoadSceneMode m) {
            Debug.Log("Scene loaded : " + sc.name);
            this.ChangeStateTo(false);
        }


        /// <summary>
        /// Delegate function on scene unloaded
        /// </summary>
        /// <param name="sc"></param>
        void OnSceneUnloaded(Scene sc) { Debug.Log("Scene unloaded : " + sc.name); }


        /// <summary>
        /// Change the active state of child elements
        /// </summary>
        /// <param name="state"></param>
        private void ChangeStateTo(bool state) {
            foreach ( Transform child in transform ) { child.gameObject.SetActive(state); }
        }

    }
}