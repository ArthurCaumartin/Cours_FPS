
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;

    [Space]
    [SerializeField] private List<Weapon> _weaponList = new List<Weapon>();
    private int _currentWeaponIndex = 0;
    private Weapon _currentWeapon;
    private bool _isShooting = false;
    private bool _isShootingSecondary = false;

    private void Start()
    {
        foreach (var item in _weaponList)
        {
            item.SetPlayerRef(_playerLook);
            item.gameObject.SetActive(false);
        }
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
        _currentWeapon?.Shoot(_isShooting);
        _currentWeapon?.ShootSecondary(_isShootingSecondary);
    }

    private void OnShoot(InputValue value)
    {
        // print(value.Get<float>());
        _isShooting = value.Get<float>() > .5f;
    }

    private void OnShootSecondary(InputValue value)
    {
        _isShootingSecondary = value.Get<float>() > .5f;
    }

    private void OnScroll(InputValue value)
    {
        float scrollValue = value.Get<float>();
        _currentWeaponIndex = (_currentWeaponIndex += (int)scrollValue) % _weaponList.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = _weaponList.Count - 1;
        SwitchWeapon(_currentWeaponIndex);
    }
}


