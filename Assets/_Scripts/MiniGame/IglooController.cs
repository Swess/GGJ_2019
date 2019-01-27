using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class IglooController : MonoBehaviour {

    public CircleManager circleManager;
    private bool _inMiniGame = false;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start() {
        _playerController = Core.GameController.Instance.Player1.GetComponent<PlayerController>();
        circleManager.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update() { }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        _inMiniGame = true;
        circleManager.gameObject.SetActive(true);
        _playerController.SetControlsActive(false);    // Remove Player controls
    }


    private void QuitMiniGame() {
        _inMiniGame = false;
        circleManager.gameObject.SetActive(false);
        _playerController.SetControlsActive(true);     // Reset Game Controls
    }

}