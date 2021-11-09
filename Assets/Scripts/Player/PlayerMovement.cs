using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float movementSpeed = 6f;
        public Vector3 movement = Vector3.zero;
        public float rigidbodyDrag = 6f;
        public float speedMultiplier = 10f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            InputManager();
            RigidbodyDragController();
        }

        private void FixedUpdate()
        {
            PlayerMove();
        }

        private void InputManager()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            movement = transform.forward * vertical + transform.right * horizontal;
        }

        private void PlayerMove()
        {
            _rigidbody.AddForce(movement.normalized * movementSpeed * speedMultiplier, ForceMode.Acceleration);
        }

        private void RigidbodyDragController()
        {
            _rigidbody.drag = rigidbodyDrag;
        }
    }
}
