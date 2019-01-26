using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class PickupPile : MonoBehaviour {

    public int amount = 3;
    public GameObject prefab;


    // Start is called before the first frame update
    void Start() { }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();
        controller.SetActionVisuals(true);
    }


    private void OnTriggerExit(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();
        controller.SetActionVisuals(false);
    }

}