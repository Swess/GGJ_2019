using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class WindZoneTrigger : MonoBehaviour {

    public  float            speed = 3f;
    private WindZone         _windZone;
    private Vector3          _dir = Vector3.zero;
    private PlayerController _playerController;


    private void Start() {
        _windZone          = gameObject.GetComponent<WindZone>();
        _windZone.windMain = 0;
    }


    public void SetWindDirection(Vector3 dir) { _dir = dir; }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        _playerController = other.GetComponent<PlayerController>();

        _playerController.windModifier = _dir.normalized * speed;
        _windZone.windMain             = 1;
    }


    private void OnTriggerExit(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        _playerController = other.GetComponent<PlayerController>();

        _playerController.windModifier = Vector3.zero;
        _windZone.windMain             = 0;
    }


    private void OnDestroy() { _playerController.windModifier = Vector3.zero; }

}