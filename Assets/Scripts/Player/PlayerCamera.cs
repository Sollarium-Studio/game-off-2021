using System;
using Config;
using Save;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Variables")] public float mouseXSensitivity;
        public float mouseYSensitivity;
        public float multiplier = 0.01f;
        public float mouseXRotation;
        public float mouseYRotation;
        public bool invertXAxis;
        public bool hideCursor;
        [Header("Components")] public Camera playerCamera;

        private void Awake()
        {
            DataManager.instance.updateConfigurations += UpdatePlayerConfigurations;
        }

        private void Start()
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            InputManager();
            MakeRotation();
        }

        private void InputManager()
        {
            //Movement Manager
            var mouseX = Input.GetAxisRaw("Mouse X");
            var mouseY = Input.GetAxisRaw("Mouse Y");
            if (invertXAxis)
                mouseXRotation += mouseY * mouseXSensitivity * multiplier;
            else
                mouseXRotation -= mouseY * mouseXSensitivity * multiplier;
            mouseYRotation += mouseX * mouseYSensitivity * multiplier;
            mouseXRotation = Mathf.Clamp(mouseXRotation, -90f, 90f);
            //Cursor manager
            if (Input.GetKeyDown(KeyCode.P)) hideCursor = !hideCursor;
            Cursor.lockState = hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !hideCursor;
        }

        private void MakeRotation()
        {
            playerCamera.transform.localRotation = Quaternion.Euler(mouseXRotation, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, mouseYRotation, 0f);
        }

        public void UpdatePlayerConfigurations()
        {
            Debug.Log($"Update config called.");
            mouseXSensitivity = DataManager.instance.data.configFile.mouseXSensitivity;
            mouseYSensitivity = DataManager.instance.data.configFile.mouseYSensitivity;
            invertXAxis = DataManager.instance.data.configFile.invertXAxisMouse;
        }

        private void OnApplicationQuit()
        {
            var save = DataManager.instance.data;
            save.configFile.mouseXSensitivity = mouseXSensitivity;
            save.configFile.mouseYSensitivity = mouseYSensitivity;
            save.configFile.invertXAxisMouse = invertXAxis;
            SaveSerialization.Save("test", save);
        }
    }
}