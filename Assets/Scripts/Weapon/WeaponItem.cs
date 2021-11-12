using System;
using Player;
using UnityEngine;

namespace Weapon
{
    public class WeaponItem : MonoBehaviour
    {
        [Header("Player")]
        public GameObject player;
        public PlayerWeapons playerWeapons;
        public PlayerCamera playerCamera;
        
        [Header("Weapon Configuration")] 
        public WeaponType weaponType;
        public FiringMode firingMode;
        public float firingRate = 5f;
        public float range = 1000f;
        public GameObject hitPrefab;

        public bool _isFiring;
        public float _timeToFiring;
        
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
            InputManager();
            WeaponFiring();
        }

        private void AnimationManager()
        {
            if (!_rigidbody) return;
            var magnitude = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.z).magnitude;
            _animator.SetBool(Walking, magnitude > 0.1f);
        }

        private void InputManager()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireBullet();
                _isFiring = true;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                _isFiring = false;
            }
        }

        private void WeaponFiring()
        {
            if (!_isFiring)
            {
                _timeToFiring = 0f;
                return;
            }
            if (firingMode == FiringMode.Single)
            {
                _isFiring = false;
            }
            else
            {
                _timeToFiring += Time.deltaTime;
                var time =  1 / firingRate;
                if (!(_timeToFiring >= (time))) return;
                FireBullet();
                _timeToFiring = 0f;
            }
        }

        private void FireBullet()
        {
            if (!playerCamera) return;
            RaycastHit hit;
            if (!Physics.Raycast(playerCamera.playerCamera.transform.position, playerCamera.playerCamera.transform.forward, out hit,
                range)) return;
            if (hit.collider.CompareTag("Player")) return;
            Instantiate(hitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    public enum WeaponType
    {
        Pistol,
        MachineGun,
        Shotgun,
    }

    public enum FiringMode
    {
        Single,
        Burst,
        Automatic
    }
}