using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
	public class Position : MonoBehaviour {
	    // Position 
	    void OnDrawGizmos() {
	        Gizmos.DrawWireSphere(transform.position, 1);
	    }

	}
}