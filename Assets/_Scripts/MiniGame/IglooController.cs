using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglooController : MonoBehaviour {

    public GameObject[] igloos;
    private int _currentStageIgloo;
    private Transform _visuals;
    // Start is called before the first frame update
    void Start() {
        _visuals = transform.Find("Visuals");
        Instantiate(igloos[_currentStageIgloo], _visuals);
    }
    // Update is called once per frame
    void Update() {
    }


    private void increaseCurrentStageIgloo() {
        if ( _currentStageIgloo == igloos.Length ) {
            Debug.LogError("YOU ARE TRYING TO GO OVER 9000!!!");
            return    ;
        }
        Destroy(_visuals.GetChild(0).gameObject);
        Instantiate(igloos[++_currentStageIgloo], _visuals);
    }
}