using System;
using UnityEngine;
using System.Collections;

public class DrawCircle : MonoBehaviour
{

    private int           _size;
    private LineRenderer  _lineDrawer;
    private float         _theta      = 0f;
    private int           _nTriangles = 20;
    private float         _thetaScale = 0.01f;
    private float         _radius     = 3f;
    private CircleManager _cm;

    private void Start ()
    {       
        _lineDrawer = GetComponent<LineRenderer>();
        try {
            _cm = GetComponentInParent<CircleManager>();
        } catch { Debug.LogError(transform.name + " have no daddy"); }

    }


    private void Update() {
        
        _nTriangles = _cm.nTriangles;
        _thetaScale = _cm.thetaScale;
        _radius     = _cm.radius;

        DrawACirclePiePart();
    }


    private void DrawACirclePiePart() {
        _theta                    = 0f;
        _size                     = (int) (1f / _thetaScale + 1f);
        _lineDrawer.positionCount = _size/ _nTriangles;
        for ( int i = 0; i < _size / _nTriangles; i++ ) {
            _theta += 2.0f * Mathf.PI * _thetaScale;
            float x = _radius * Mathf.Cos(_theta);
            float y = _radius * Mathf.Sin(_theta);
            _lineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
    
}