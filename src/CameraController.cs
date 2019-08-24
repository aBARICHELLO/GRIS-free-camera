using System;
using System.Collections.Generic;
using Gris.SaveGame;
using Gris.SimpleJSON;
using UnityEngine;

namespace Gris {
    [RequireComponent(typeof(Camera))]
    public partial class CameraController : MonoBehaviourSaveGameListener {

        // (...)

        void LateUpdate() {
            if (!Utils.IsGameRunning()) {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Numlock)) {
                this.noclip = !this.noclip;
            }
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                this.followMode = !this.followMode;
            }
            if (this.noclip) {
                this.NoclipUpdate(ref this.m_Camera);
                return;
            }
            // (...)
        }

        void NoclipUpdate(ref Camera camera) {
            // Speed
            float speedModifier = 1f;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Mouse3)) {
                speedModifier = 20f;
            }

            // Transform
            if (Input.GetKey(KeyCode.KeypadMinus) || Input.mouseScrollDelta.y < 0f) {
                Vector3 direction = camera.transform.forward;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.KeypadPlus) || Input.mouseScrollDelta.y > 0f) {
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
            if (Input.GetKey(KeyCode.UpArrow)) {
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * noclipCameraSpeed * speedModifier * direction;
            }

            if (this.followMode) {
                CameraController.CameraSettings currentSettings = this.GetCurrentSettings();
                Vector3 vector = this.CalculateCameraPosition(currentSettings, ref this.m_CurrentCameraTransientData, true);
                camera.transform.position = new Vector3(vector.x, camera.transform.position.y, camera.transform.position.z);
            }
        }

        // (...)

        bool followMode = false;
        bool noclip = false;
        const float noclipCameraSpeed = 10f;
    }
