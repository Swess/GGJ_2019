using Core;
using UnityEngine;

namespace UI {
    public class UILogicBridge : MonoBehaviour {

        public void LoadScene(string sceneName) { GameController.Instance.SceneController.FadeAndLoadScene(sceneName); }

        public void KillApp(){ GameController.Instance.KillGame(); }

    }
}