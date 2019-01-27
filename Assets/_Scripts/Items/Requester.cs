using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Entities.Player;
using UnityEngine;
using UnityEngine.UI;

public class Requester : MonoBehaviour {

    private const string REQUEST_NONE = "NONE";

    public int           askingEachNSeconds = 20;
    public int           fulfillingTime     = 40;
    public string[]      requestingTag;
    public RectTransform bubbleFrame;
    public Image iconDisplay;

    public Sprite alertSprite;
    public Sprite woodSprite;
    public Sprite foodSprite;


    private float       _timer = 0;
    private IEnumerator _coroutine;

    private string           _requested = REQUEST_NONE;
    private bool _hasShownItem = false;
    private PlayerController _player;

    private void Start() {
        _coroutine = RequestTimer();
        StartCoroutine(_coroutine);

        _player = GameController.Instance.Player1.GetComponent<PlayerController>();
    }


    private void Update() {
        if ( _timer > askingEachNSeconds && _requested.Equals(REQUEST_NONE) ) RequestItem();

        if ( _timer > askingEachNSeconds + fulfillingTime ) GameController.Instance.Loss();

        MoveAlertBubble();
    }


    private void MoveAlertBubble() {
        if ( Camera.main == null ) return;

        Vector3 screenPos       = Camera.main.WorldToScreenPoint(transform.position);
        screenPos = WorldToScreenPointProjected(Camera.main, transform.position);
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(GameController.Instance.Player1.transform.position);

        Vector3 direction = playerScreenPos - screenPos;
        float   angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector2 finalScreenPos = new Vector2(screenPos.x, screenPos.y);

        // Clamp to screen
       
        
        if ( finalScreenPos.x >= Screen.width ) finalScreenPos.x = Screen.width;
        if ( finalScreenPos.x <= 0 ) finalScreenPos.x            = 0f;

        if ( finalScreenPos.y >= Screen.height ) finalScreenPos.y = Screen.height;
        if ( finalScreenPos.y <= 0 ) finalScreenPos.y             = 0f;

        bubbleFrame.position = new Vector3(finalScreenPos.x, finalScreenPos.y, 0);
        bubbleFrame.rotation = Quaternion.Euler(0, 0, angle + 90);


        // Icon display cases
        ///// If close to family
        if ( !_requested.Equals(REQUEST_NONE) ) {
            bubbleFrame.gameObject.SetActive(true);

            // Should we display the item icon ?
            if ( _hasShownItem || direction.magnitude < Screen.width/10f ) {
                switch ( _requested ) {
                    case "Wood_Item":
                        iconDisplay.sprite = woodSprite;
                        break;
                    case "Food_Item":
                        iconDisplay.sprite = foodSprite;
                        break;
                    default:
                        iconDisplay.sprite = alertSprite;
                        break;
                }

                _hasShownItem = true;
            } else {
                iconDisplay.sprite = alertSprite;
            }
        } else {
            bubbleFrame.gameObject.SetActive(false);
        }
    }

    public static Vector2 WorldToScreenPointProjected( Camera camera, Vector3 worldPos )
    {
        Vector3 camNormal     = camera.transform.forward;
        Vector3 vectorFromCam = worldPos - camera.transform.position;
        float   camNormDot    = Vector3.Dot( camNormal, vectorFromCam );
        if ( camNormDot <= 0 )
        {
            // we are behind the camera forward facing plane, project the position in front of the plane
            Vector3 proj = ( camNormal * camNormDot * 1.01f );
            worldPos = camera.transform.position + ( vectorFromCam - proj );
        }
 
        return RectTransformUtility.WorldToScreenPoint( camera, worldPos );
    }


    private IEnumerator RequestTimer() {
        while ( true ) {
            yield return new WaitForSeconds(0.1f);

            _timer += 0.1f;
        }
    }


    private void RequestItem() {
        // Ask the opposite of player's holding item
        if ( _player.GetCurrentObjectTag().Equals("Food_Item") ) {
            Debug.Log("Case 1");
            _requested = "Wood_Item";
        } else if ( _player.GetCurrentObjectTag().Equals("Wood_Item") ) {
            Debug.Log("Case 2");
            _requested = "Food_Item";
        } else {
            // Random otherwise
            _requested = requestingTag[Random.Range(0, requestingTag.Length)];
        }

        _hasShownItem = false;
        Debug.Log("Requesting : " + _requested);
    }


    public void Receive(string receivingTag) {
        // Receives the requested object
        // TODO: Received event logic

        _requested = REQUEST_NONE;
        _timer     = 0;

        Debug.Log("Received : " + receivingTag);
    }


    private void OnTriggerEnter(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();

        // Check if the player has the requested Item
        if ( _requested.Equals(controller.GetCurrentObjectTag()) ) {
            controller.SetUsageVisuals(this, true);
        }
    }


    private void OnTriggerExit(Collider other) {
        if ( !other.CompareTag("Player") ) return;

        PlayerController controller = other.GetComponent<PlayerController>();
        controller.SetUsageVisuals(this, false);
    }

}