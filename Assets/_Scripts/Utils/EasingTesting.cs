using UnityEngine;
using Utils;

public class EasingTesting : MonoBehaviour {

    private Rigidbody2D _rb;


    private void Awake() { _rb = GetComponent<Rigidbody2D>(); }


    // Update is called once per frame
    void FixedUpdate() {
        if ( Input.GetButtonDown("Jump") ) {
            Vector2 pos = _rb.position;
            pos.x = EasingFunction.EaseInExpo(pos.x, pos.x + 5, pos.x);
            _rb.MovePosition(pos);
        }
    }


    // animate the game object from -1 to +1 and back
    public float minimum = -1.0F;
    public float maximum = 1.0F;
    public EasingFunction.Ease ease;

    private EasingFunction.Function _easeFunc;

    // starting value for the Lerp
    private float t = 0.0f;


    private void Start() {
        _easeFunc = EasingFunction.GetEasingFunction(ease);
    }


    void Update() {
        // animate the position of the game object...
        transform.position = new Vector3(_easeFunc(minimum, maximum, t), transform.position.y, transform.position.z);

        // .. and increate the t interpolater
        t += 0.5f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if ( t > 1.0f ) {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t       = 0.0f;
        }
    }

}