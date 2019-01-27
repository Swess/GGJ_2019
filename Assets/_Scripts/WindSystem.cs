using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour {

    public GameObject windPrefab;
    public float      delay = 8f;
    public Transform  centerPosition;
    public float      maxWidth  = 10;
    public float      maxHeight = 8;

    private IEnumerator _coroutine;
    private float       _yValue;


    private void Start() {
        _coroutine = Delaying();
        StartCoroutine(_coroutine);
        ChangeWindsCols();
    }


    IEnumerator Delaying() {
        while ( true ) {
            yield return new WaitForSeconds(delay);

            ChangeWindsCols();
        }
    }


    private void ChangeWindsCols() {
        DeleteChilds();

        Vector3 pos1 = new Vector3(centerPosition.position.x - Random.Range(0, maxWidth),
                                   _yValue,
                                   centerPosition.position.y + Random.Range(-maxHeight, maxHeight));

        Vector3 pos2 = new Vector3(centerPosition.position.x + Random.Range(0, maxWidth),
                                   _yValue,
                                   centerPosition.position.y + Random.Range(-maxHeight, maxHeight));


        Vector3 dir1 = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
        Vector3 dir2 = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        Quaternion rot1 = Quaternion.LookRotation(dir1, Vector3.up);
        Quaternion rot2 = Quaternion.LookRotation(dir2, Vector3.up);


        GameObject inst1 = Instantiate(windPrefab, pos1, rot1, transform);
        inst1.GetComponent<WindZoneTrigger>().SetWindDirection(dir1);

        GameObject inst2 = Instantiate(windPrefab, pos2, rot2, transform);
        inst2.GetComponent<WindZoneTrigger>().SetWindDirection(dir2);
    }


    private void DeleteChilds() {
        for ( int i = 0; i < transform.childCount; i++ ) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}