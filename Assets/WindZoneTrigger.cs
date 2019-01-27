using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class WindZoneTrigger : MonoBehaviour {

    public  float            speed = 3f;
    private WindZone         _windZone;
    private Vector3          _dir = Vector3.zero;
    private PlayerController _playerController;
    private bool             _inside = true;

    private IEnumerator _coroutine;


    private void Start() {
        _windZone          = gameObject.GetComponent<WindZone>();
        _windZone.windMain = 0;

        _coroutine = Retrigger();
        StartCoroutine(_coroutine);
    }


    public void SetWindDirection(Vector3 dir) { _dir = dir; }


    IEnumerator Retrigger() {
        while ( true ) {
            yield return new WaitForSeconds(0.2f);

            if ( _playerController && _inside ) {
                _playerController.windModifier = _dir.normalized * speed;
                _windZone.windMain             = 0.15f;

                AkSoundEngine.SetRTPCValue("Wind_Level",   100);
                AkSoundEngine.SetRTPCValue("WindOnChimes", 100);
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        _playerController = other.GetComponent<PlayerController>();

        _playerController.windModifier = _dir.normalized * speed;
        _windZone.windMain             = 0.15f;

        AkSoundEngine.SetRTPCValue("Wind_Level",   100);
        AkSoundEngine.SetRTPCValue("WindOnChimes", 100);

        _inside = true;
    }


    private void OnTriggerExit(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        _playerController = other.GetComponent<PlayerController>();

        _playerController.windModifier = Vector3.zero;
        _windZone.windMain             = 0;

        AkSoundEngine.SetRTPCValue("Wind_Level",   20);
        AkSoundEngine.SetRTPCValue("WindOnChimes", 20);
        
        _inside = false;
    }


    private void OnDestroy() {
        _windZone.windMain = 0;
        _playerController.windModifier = Vector3.zero;
        AkSoundEngine.SetRTPCValue("Wind_Level",   20);
        AkSoundEngine.SetRTPCValue("WindOnChimes", 20);
    }

}