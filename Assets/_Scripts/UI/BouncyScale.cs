using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BouncyScale : MonoBehaviour {

    public float               minimum = -1.0F;
    public float               maximum = 1.0F;
    public float               speed   = 1f;
    public EasingFunction.Ease ease;

    private EasingFunction.Function _easeFunc;
    private float                   _t;

    private void Start() { _easeFunc = EasingFunction.GetEasingFunction(ease); }


    void Update() {
        transform.localScale = new Vector3(1 + _easeFunc(minimum, maximum, _t), 1 + _easeFunc(minimum, maximum, _t), transform.position.z);

        // .. and increase the t interpolater
        _t += speed * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if ( _t > 1.0f ) {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            _t      = 0.0f;
        }
    }

}