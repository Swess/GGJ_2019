using System;
using Core;
using UnityEngine;

namespace Cameras {
    public class CameraController : MonoBehaviour {

        public const int ROTATION_ANGLE = 90;

        public float minCameraDistance = 10f;

        // Time that a rotation takes to complete
        public float rotatingSpeed  = 5f;
        public float translateSpeed = 0.5f;

        public GameObject mapCenterReference;
        public float maxWidthClamp = 15f;
        public float maxHeightClamp = 15f;

        private Rewired.Player _player1;
        private Vector3        _wantedAngle;
        private Vector3        _camVel = Vector3.zero;

        public Vector3 ForwardVector { get; protected set; }

        // ====================================
        // ====================================


        private void Awake() {
            GameController.Instance.SceneController.AfterSceneLoad += OnSceneLoaded;
        }


        private void Update() {
            CheckForRotation();
            CenterToPlayers();
        }


        // ====================================
        // ====================================


        public void OnSceneLoaded() {
            _player1 = GameController.Instance.actionsMapsHelper.Player1Inputs;

            // Take in consideration initial angles
            _wantedAngle = transform.rotation.eulerAngles * Mathf.Deg2Rad;
        }


        /// <summary>
        /// Check for inputs & updates rotation
        /// </summary>
        private void CheckForRotation() {
            if ( _player1.GetButtonDown("RotateCamLeft") ) {
                _wantedAngle.y += ROTATION_ANGLE * Mathf.Deg2Rad;
                ForwardVector  = Quaternion.AngleAxis(ROTATION_ANGLE, Vector3.up) * ForwardVector;
            }

            if ( _player1.GetButtonDown("RotateCamRight") ) {
                _wantedAngle.y -= ROTATION_ANGLE * Mathf.Deg2Rad;
                ForwardVector  = Quaternion.AngleAxis(-ROTATION_ANGLE, Vector3.up) * ForwardVector;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.Euler(_wantedAngle * Mathf.Rad2Deg),
                                                 rotatingSpeed * Time.deltaTime);
        }


        /// <summary>
        /// Center the camera to the characters mid point
        /// </summary>
        private void CenterToPlayers() {
            GameObject p1 = GameController.Instance.Player1;

            Vector3 mid = p1.transform.position;

            double y      = Math.Sin(_wantedAngle.x) * minCameraDistance;
            double xzDist = Math.Cos(_wantedAngle.x) * minCameraDistance;

            Vector3 offset = new Vector3((float) (Math.Sin(_wantedAngle.y + Math.PI) * xzDist),
                                         (float) y,
                                         (float) (Math.Cos(_wantedAngle.y + Math.PI) * xzDist));


            // Limit bounds of follow

            Vector3 targetPos = mid + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _camVel, translateSpeed);
        }

    }
}