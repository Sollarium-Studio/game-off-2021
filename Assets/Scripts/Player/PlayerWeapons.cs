using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        public GameObject weaponsParent;
        public List<WeaponItem> weapons;
        public int currentWeapon;
        public int oldWeapon;

        private void Start()
        {
            weapons = weaponsParent.GetComponentsInChildren<WeaponItem>().ToList();
            weapons.ForEach(weapon =>
            {
                weapon.player = gameObject;
                weapon.gameObject.SetActive(weapon == weapons[0]);
            });
        }

        private void Update()
        {
            ChangeWeapons();
            ChangeQuickWeapon();
        }

        private void ChangeWeapons()
        {
            var scroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scroll > 0)
            {
                oldWeapon = currentWeapon;
                currentWeapon++;
                currentWeapon = currentWeapon == weapons.Count ? 0 : currentWeapon;
            }
            if (scroll < 0)
            {
                oldWeapon = currentWeapon;
                currentWeapon--;
                currentWeapon = currentWeapon < 0 ? weapons.Count - 1 : currentWeapon;
            }

            if (oldWeapon == currentWeapon) return;
            SetNewWeapon(oldWeapon,currentWeapon);
        }

        private void ChangeQuickWeapon()
        {
            if (!Input.anyKeyDown) return;
            if (Input.GetAxisRaw("Change Weapon") == 0) return;
            if (oldWeapon == currentWeapon) return;
            SetNewWeapon(oldWeapon,currentWeapon);
            (oldWeapon, currentWeapon) = (currentWeapon, oldWeapon);
        }

        private void SetNewWeapon(int old, int current)
        {
            weapons[old].gameObject.SetActive(false);
            weapons[current].gameObject.SetActive(true);
        }
    }
}