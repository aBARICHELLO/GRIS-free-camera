using System;
using System.Collections.Generic;
using Gris.SaveGame;
using Gris.SimpleJSON;
using UnityEngine;

namespace Gris {
    [RequireComponent(typeof(Camera))]
    public partial class CameraController : MonoBehaviourSaveGameListener {

        void LateUpdate() {
            if (!Utils.IsGameRunning()) {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Numlock)) {
                this.noclip = !this.noclip;
            }
            if (this.noclip) {
                this.NoclipUpdate(ref this.m_Camera);
                return;
            }
            // (...)
        }

        void NoclipUpdate(ref Camera camera) {
            float speedModifier = 1f;
            if (Input.GetKey(KeyCode.LeftShift)) {
                speedModifier = 10f;
            }

            // Transform
            if (Input.GetKey(KeyCode.KeypadMinus)) {
                Vector3 direction = camera.transform.forward;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.KeypadPlus)) {
                Vector3 direction = camera.transform.forward;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Vector3 direction = camera.transform.right;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                Vector3 direction = camera.transform.right;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
        }

        bool noclip = false;
        const float noclipCameraSpeed = 10f;
    }
