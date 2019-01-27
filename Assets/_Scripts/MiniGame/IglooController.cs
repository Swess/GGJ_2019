using System.Collections;
using System.Collections.Generic;
using Core;
using Entities.Player;
using UnityEngine;

public class IglooController : MonoBehaviour {

    public CircleManager circleManager;
    private bool _inMiniGame = false;
    private PlayerController _playerController;

    public GameObject[] igloos;
    private int _currentStageIgloo;
    private Transform _visuals;
    // Start is called before the first frame update
    void Start() {
        _visuals = transform.Find("Visuals");
        Instantiate(igloos[_currentStageIgloo], _visuals);
        _playerController = Core.GameController.Instance.Player1.GetComponent<PlayerController>();
        circleManager.gameObject.SetActive(false);
        circleManager.miniGameWinning.AddListener(QuitMiniGame);
        GameController.Instance.startTimer();
    }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        if ( !_playerController.GetCurrentObjectTag().Equals("Snow_Item") ) return;

        _inMiniGame = true;
        circleManager.gameObject.SetActive(true);
        _playerController.SetControlsActive(false);    // Remove Player controls
    }

    private void QuitMiniGame() {
        _inMiniGame = false;
        circleManager.gameObject.SetActive(false);
        _playerController.SetControlsActive(true);     // Reset Game Controls
        increaseCurrentStageIgloo();
        checkWinCondition();
    }

    private void checkWinCondition() {
        if ( _currentStageIgloo == igloos.Length ) GameController.Instance.Win();
    }
    
    private void increaseCurrentStageIgloo() {
        if ( _currentStageIgloo == igloos.Length ) {
            Debug.LogError("YOU ARE TRYING TO GO OVER 9000!!!");
            return;
        }
        Destroy(_visuals.GetChild(0).gameObject);
        Instantiate(igloos[++_currentStageIgloo], _visuals);
    }
}