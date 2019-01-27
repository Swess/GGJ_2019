using System.Collections;
using Core;
using UnityEngine;
using Rewired;

namespace Entities.Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : EntitiesController {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        public                float    maxVelocity    = 10f;
        public                float    windSpeed      = 5f;
        [Range(0, 20)] public float    frictionFactor = 4f;
        public                Animator animator;
        public                Vector3  windModifier = Vector3.zero;

        private Rigidbody  _rb;
        private bool       _listensInputs = true;
        private PickupPile _pickupZone;
        private Requester  _requesterZone;
        private GameObject _currentItem;
        private Transform  _itemHolder;

        private Vector3 _previousDirection;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            _rb         = GetComponent<Rigidbody>();
            _itemHolder = transform.Find("ItemHolder");

            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
            // PlayerInputs = GameController.Instance.actionsMapsHelper.Player1Inputs; // Get the MainPlayer's inputs
        }


        private void Update() {
            if (_listensInputs) {
                CheckForItemPickup();
                CheckItemUsage();
                CheckForItemDrop();
            }
        }


        private void FixedUpdate() {
            SlowDownPlayer();
            CheckForMovement();
        }


        // ========================================================
        // ========================================================
        // ========================================================


        public string GetCurrentObjectTag() { return !_currentItem ? "" : _currentItem.tag; }


        /// <summary>
        /// Gradually slowDown player
        /// </summary>
        private void SlowDownPlayer() {
            Vector3 opposite = -_rb.velocity;
            _rb.AddForce(opposite * frictionFactor);
        }


        private void CheckForMovement() {
            // Check here
            Vector3 forceAxis;
            if(_listensInputs)
                forceAxis = new Vector3(PlayerInputs.GetAxisRaw("Horizontal"), 0, PlayerInputs.GetAxisRaw("Vertical"));
            else
                forceAxis = new Vector3(0, 0, 0);


            // Apply Movement
            _rb.velocity = forceAxis.normalized * maxVelocity;

            // Change Rotation
            if ( forceAxis.magnitude > 0 ) _previousDirection = forceAxis.normalized;

            // Apply wind ON TOP of movement velocity
            _rb.velocity += windModifier.normalized * windSpeed;


            // animator.SetBool("Idle", _rb.velocity.magnitude < 0.1f );


            // Rotate Object
            if ( _rb.velocity.magnitude > 0.2f ) {
                float angle = Mathf.Atan2(_rb.velocity.z, _rb.velocity.x) * Mathf.Rad2Deg * -1 + 90;
                transform.eulerAngles = new Vector3(0, angle, 0);
            }
        }


        private void CheckForItemPickup() {
            if ( _pickupZone && PlayerInputs.GetButtonDown(RewiredConsts.Action.Use) ) {
                EmptyItemHolder();
                _currentItem = Instantiate(_pickupZone.prefab, _itemHolder.position, Quaternion.identity, _itemHolder);

                _pickupZone.UseOne();
                SetPickupVisuals(_pickupZone, false);
                _pickupZone = null;
            }
        }


        private void CheckItemUsage() {
            if ( _requesterZone && PlayerInputs.GetButtonDown(RewiredConsts.Action.Use) ) {
                EmptyItemHolder();

                _requesterZone.Receive(_currentItem.tag);
                SetUsageVisuals(_requesterZone, false);
                _requesterZone = null;


                // Play Audio
                if ( _currentItem.tag.Equals("Food_Item") ) {
                    AkSoundEngine.PostEvent("Play_FoodDrop", gameObject);
                } else if ( _currentItem.tag.Equals("Wood_Item") ) {
                    AkSoundEngine.PostEvent("Play_WoodDrop", gameObject);
                }
            }
        }


        private void CheckForItemDrop() {
            if ( _currentItem && PlayerInputs.GetButtonDown(RewiredConsts.Action.Drop) ) {
                EmptyItemHolder();
                _currentItem = null;
            }
        }


        public void EmptyHands() {
            EmptyItemHolder();
            _currentItem = null;
        }

        public Vector3 GetDirection() { return _rb.velocity.normalized; }


        /// <summary>
        /// Display Character visual queue for pickup actions
        /// </summary>
        public void SetPickupVisuals(PickupPile pp, bool state) {
            _pickupZone = state ? pp : null;
            transform.Find("Canvas").gameObject.SetActive(state);
        }


        /// <summary>
        /// Display Character visual queue for Usage actions
        /// </summary>
        public void SetUsageVisuals(Requester requester, bool state) {
            _requesterZone = state ? requester : null;
            transform.Find("Canvas").gameObject.SetActive(state);
        }


        IEnumerator DestroyPPVolume() {
            float counter = 0f;
            float fxTime  = 0.4f;

            while ( counter < fxTime ) {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }


        private void EmptyItemHolder() {
            for ( int i = 0; i < _itemHolder.childCount; i++ ) {
                Destroy(_itemHolder.GetChild(i).gameObject);
            }
        }


        /// <summary>
        /// Determine if the player controller listens to inputs
        /// </summary>
        /// <param name="state"></param>
        public void SetControlsActive(bool state) { _listensInputs = state; }

    }
}