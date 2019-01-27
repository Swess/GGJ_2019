using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Rewired;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class CircleManager : MonoBehaviour
{
    public  int          nTriangles = 20;
    public int correction = 0;
    public  GameObject   prefab;
    public GameObject spriteManager;
    public int answerLenght = 5;
    public float waitTime = 0.75f;
    
    private GameObject[] _piePart;
    private Player _player1Input;

    private int[] _answer;
    private int _currentIndexChallenge =0;
    private bool _miniGameWon = false;

    private SpriteRenderer _currentSymbol;
    
    private void Start () {

        _player1Input = Core.GameController.Instance.actionsMapsHelper.Player1Inputs;
        _piePart      = new GameObject[nTriangles];
        _currentSymbol = transform.Find("CurrentSymbol").transform.GetComponent<SpriteRenderer>();

        ResetState();
        SpawnChildren();
        flipIt(); //might be problematic? hell nah
    }

    private void Update() {
        if ( _miniGameWon ) {
            Debug.Log("MINI GAME WON!!!!");
            ResetState();
        }
        
        int realIndex = getIndex();
        
        UpdateLookAt(realIndex);
        
        if ( _player1Input.GetButtonDown(RewiredConsts.Action.Use)) {
            Select(realIndex);
        }
    }

//
//    public int[] getCurrentWord() {
//        if ( _currentIndexChallenge == 0 ) return null;
//        int[] currentWord = new int[_currentIndexChallenge-1];
//        
//    }
    
    private void flipIt() { transform.rotation = Quaternion.Euler(0, 180, 0); }

    private void ResetState() {
        _answer = PickNewAnswer();
        _currentIndexChallenge = 0;
        UpdateChallengeSprite();
        _miniGameWon = false;
    }
    
    
    private int[] PickNewAnswer() {
        Random random = new Random();
  
        int[] answer = new int[answerLenght];
        for ( int i = 0; i < answer.Length; i++ ) {
            int randomNumber = random.Next(0, nTriangles - 1);
            if ( i == 0 ) {
                answer[i] = randomNumber;
                continue;
            }
            while(answer[i-1] == randomNumber) randomNumber = random.Next(0, nTriangles - 1);
            answer[i] = randomNumber;
        }
        return answer;
    }

    private void Select(int realIndex) {
        if ( _answer[_currentIndexChallenge] == realIndex) {
            goodAnswer(realIndex);
        } else {
            badAnswer(realIndex);
        }
    }


    private void badAnswer(int realIndex) {
        Debug.Log("Not so great answer...");
        _piePart[realIndex].transform.Find("Bad").gameObject.SetActive(true);
        _piePart[realIndex].transform.Find("Good").gameObject.SetActive(false);
    }


    private void goodAnswer(int realIndex) {
        _currentIndexChallenge++;
        _piePart[realIndex].transform.Find("Bad").gameObject.SetActive(false);
        _piePart[realIndex].transform.Find("Highlight").gameObject.SetActive(false);
        _piePart[realIndex].transform.Find("Good").gameObject.SetActive(true);
        Invoke("setEverythingGray", waitTime);
        if ( _currentIndexChallenge == _answer.Length ) {
            miniGameWon();
        } else {
            UpdateChallengeSprite();
        }
    }


    private void setEverythingGray() {
        foreach ( GameObject go in _piePart ) {
            go.transform.Find("Bad").gameObject.SetActive(false);
            go.transform.Find("Good").gameObject.SetActive(false);
        }
    }

    private void UpdateChallengeSprite() {
        _currentSymbol.sprite = spriteManager.GetComponent<SpriteManagerScript>().sprites[ _answer[_currentIndexChallenge] ];
    }
    
    private void miniGameWon() { _miniGameWon = true; }


    private void UpdateLookAt(int realIndex) {
        foreach ( GameObject go in _piePart ) {
            GameObject highlight = go.transform.Find("Highlight").gameObject;
            highlight.SetActive(false);
        }

        if ( realIndex == -1 ) {
            return;
        }

        _piePart[realIndex].transform.Find("Highlight").gameObject.SetActive(true);
    }


    private int getIndex() {
        float angle = getAngle();
        if ( angle > 360 ) return -1;
        int                 geniusCompute = 360 / nTriangles;
        int                 index         = (int) (getAngle() / geniusCompute);
        CustomRealmOfNumber realIndex     = new CustomRealmOfNumber(index);
        realIndex.Add(correction);
        return realIndex.Value;
    }


    private float getAngle() {
        if ( _player1Input == null ) return 1000f;

        float   horizontal = _player1Input.GetAxisRaw(RewiredConsts.Action.Horizontal);
        float   vertical   = _player1Input.GetAxisRaw(RewiredConsts.Action.Vertical);
        Vector2 pVector2   = new Vector2(horizontal, vertical);

        float angle ;
        
        if ( pVector2.Equals(Vector2.zero) ) return 1000f;
        
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
            _piePart[i] = PieConfig(angle, i);
            angle += 360 / nTriangles;
        }
    }


    private GameObject PieConfig(int angle, int index) {
        GameObject go = Instantiate(prefab, transform.position, Quaternion.Euler(0, 0, angle), transform);
        go.transform.name = "Child at angle: " + angle;
        go.transform.Find("Symbol").GetComponent<SpriteRenderer>().sprite =
        spriteManager.GetComponent<SpriteManagerScript>().sprites[index];
        return go;
    }

}