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

        public Vector3 windModifier = Vector3.zero;

        private Rigidbody _rb;
        // private PlayerItemHolder _holder;

        private Vector3 _previousDirection;

        // public PostProcessVolume ppVolume;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            _rb = GetComponent<Rigidbody>();
            // _holder     = GetComponentInChildren<PlayerItemHolder>();
            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
            // PlayerInputs = GameController.Instance.actionsMapsHelper.Player1Inputs; // Get the MainPlayer's inputs
        }


        private void Start() { }


        private void Update() { CheckForUseItem(); }


        private void FixedUpdate() {
            SlowDownPlayer();
            CheckForMovement();
        }


        // ========================================================
        // ========================================================
        // ========================================================


        /// <summary>
        /// Gradually slowDown player
        /// </summary>
        private void SlowDownPlayer() {
            Vector3 opposite = -_rb.velocity;
            _rb.AddForce(opposite * frictionFactor);
        }


        private void CheckForMovement() {
            // Check here
            Vector3 forceAxis = new Vector3(PlayerInputs.GetAxisRaw("Horizontal"), 0, PlayerInputs.GetAxisRaw("Vertical"));

            // Apply Movement
            _rb.velocity = forceAxis.normalized * maxVelocity;

            // Change Rotation
            if ( forceAxis.magnitude > 0 ) _previousDirection = forceAxis.normalized;

            // Apply wind ON TOP of movement velocity
            _rb.velocity += windModifier.normalized * windSpeed;


            // animator.SetBool("Idle", _rb.velocity.magnitude < 0.1f );
        }


        private void CheckForUseItem() {
            if ( PlayerInputs.GetButtonDown("UseItem") ) {
                // _holder.UseItem(GetDirection());
            }
        }


        public Vector3 GetDirection() { return _rb.velocity.normalized; }


        /// <summary>
        /// Display Character visual queue for pickup actions
        /// </summary>
        public void SetActionVisuals(bool state) {
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

    }
}