
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList = new List<Weapon>();
    private int _currentWeaponIndex = 0;
    private Weapon _currentWeapon;
    private bool _isShooting = false;

    private void Start()
    {
        _weaponList.ForEach(weapon => weapon.gameObject.SetActive(false));
        SwitchWeapon(_currentWeaponIndex);
    }

    private void SwitchWeapon(int index)
    {
        if (_currentWeapon)
            _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = _weaponList[index];
        _currentWeapon.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_isShooting)
            _currentWeapon?.Shoot();
    }

    private void OnShoot(InputValue value)
    {
        print(value.Get<float>());
        _isShooting = value.Get<float>() > .5f;
    }

    private void OnScroll(InputValue value)
    {
        float scrollValue = value.Get<float>();
        _currentWeaponIndex = (_currentWeaponIndex += (int)scrollValue) % _weaponList.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = _weaponList.Count - 1;
        SwitchWeapon(_currentWeaponIndex);
    }
}


