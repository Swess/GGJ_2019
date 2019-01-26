using UnityEngine;
using System.Collections;

public class CircleManager : MonoBehaviour
{
    public  int          nTriangles = 20;
    public  float        thetaScale = 0.01f;
    public  float        _radius    = 3f;
    public  GameObject   prefab;
    private GameObject[] _piePart;
    
    private void Start () {
        _piePart = new GameObject[nTriangles];
        SpawnChildren();
    }

    private void Update() {
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