using UnityEngine;
using System.Collections;
using Rewired;

public class CircleManager : MonoBehaviour
{
    public  int          nTriangles = 20;
    public  float        thetaScale = 0.01f;
    public  float        radius     = 3f;
    public  GameObject   prefab;
    private GameObject[] _piePart;

    private Player _player1Input;
    
    private void Start () {

        _player1Input = Core.GameController.Instance.actionsMapsHelper.Player1Inputs;
        _piePart      = new GameObject[nTriangles];
        SpawnChildren();
    }


    private void Update() { return getAngle(); }


    private float getAngle() {
        if ( _player1Input == null ) return;

        float   horizontal = _player1Input.GetAxisRaw(RewiredConsts.Action.Horizontal);
        float   vertical   = _player1Input.GetAxisRaw(RewiredConsts.Action.Vertical);
        Vector2 pVector2   = new Vector2(horizontal, vertical);

        float angle = 0;

        if ( pVector2.x < 0 ) {
            angle = 360 - (Mathf.Atan2(pVector2.x, pVector2.y) * Mathf.Rad2Deg * -1);
        } else {
            angle = Mathf.Atan2(pVector2.x, pVector2.y) * Mathf.Rad2Deg;
        }

        return angle;
    }


    private void SpawnChildren() {

        int angle = 0;
        for (int i = 0; i < nTriangles; i++) {
            _piePart[i] = PieConfig(angle);

            angle += 360 / nTriangles;
        }
    }


    private GameObject PieConfig(int angle) {
        GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, angle));
        go.transform.parent = transform;
        return go;
    }

}