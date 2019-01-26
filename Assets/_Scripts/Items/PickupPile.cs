using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class PickupPile : MonoBehaviour {

    public int amount = 3;
    public GameObject prefab;

    public void UseOne() {
        // TODO: trigger sound here
        amount--;
        if ( amount == 0 )
            Destroy(gameObject);

    }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();

        // Check if the player has same item type in hand
        string objTag = controller.GetCurrentObjectTag();
        if ( objTag.Length == 0 || !prefab.CompareTag(objTag) ) {
            controller.SetPickupVisuals(this, true);
        }
    }


    private void OnTriggerExit(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();
        controller.SetPickupVisuals(this, false);
    }

}