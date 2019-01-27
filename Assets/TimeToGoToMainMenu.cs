using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class TimeToGoToMainMenu : MonoBehaviour {

    public float timeToGoToMainMenu = 5f;
    
    private void Start() {
        Invoke("GoToMainMenu", timeToGoToMainMenu);
    }

    private void GoToMainMenu() {
        GameController.Instance.SceneController.FadeAndLoadScene("MainMenu");
    }

}
