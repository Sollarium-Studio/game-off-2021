using System;
using UnityEngine;

namespace Weapon
{
    public class WeaponItem : MonoBehaviour
    {
        public GameObject player;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private static readonly int Walking = Animator.StringToHash("walking");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_rigidbody) _rigidbody = player.GetComponent<Rigidbody>();
            AnimationManager();
        }

        public void AnimationManager()
        {
            if (!_rigidbody) return;
            var magnitude = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.z).magnitude;
            _animator.SetBool(Walking, magnitude > 0.1f);
        }
    }
}
