
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private AimCursor _aimCursor;

    [Space]
    [SerializeField] private List<WeaponBehavior> _weaponList = new List<WeaponBehavior>();
    private int _currentWeaponIndex = 0;
    private WeaponBehavior _currentWeapon;
    private bool _isShooting = false;
    private bool _isShootingSecondary = false;

    private void Start()
    {
        _currentWeaponIndex = 0;
        foreach (var item in GetComponentsInChildren<WeaponBehavior>())
            AddWeapon(item);
    }

    public void AddWeapon(WeaponBehavior newWeapon)
    {
        _weaponList.Add(newWeapon);
        newWeapon.transform.SetParent(transform);
        newWeapon.InitGrab(this, _playerLook, _aimCursor);

        _currentWeaponIndex = _weaponList.IndexOf(newWeapon);
        SwitchWeapon(_currentWeaponIndex);
    }

    public void RemoveWeapon(int index)
    {
        if (index < 0 || index >= _weaponList.Count) return;

        WeaponBehavior weaponToRemove = _weaponList[index];
        _weaponList.RemoveAt(index);
        weaponToRemove.DropWeapon();

        _currentWeapon = null;
        if (_currentWeaponIndex >= _weaponList.Count)
            _currentWeaponIndex = 0;
        SwitchWeapon(_currentWeaponIndex);
    }

    private void SwitchWeapon(int index)
    {
        if (index < 0 || index >= _weaponList.Count) return;
        // print("Switch weapon to index : " + index);
        if (_currentWeapon)
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon.EnableWeapon(false);
        }

        _currentWeapon = _weaponList[index];
        _currentWeapon.gameObject.SetActive(true);
        _currentWeapon.EnableWeapon(true);
    }

    private void Update()
    {
        _currentWeapon?.Shoot(_isShooting);
        _currentWeapon?.ShootSecondary(_isShootingSecondary);
    }

    private void OnShoot(InputValue value)
    {
        _isShooting = value.Get<float>() > .5f;
    }

    private void OnShootSecondary(InputValue value)
    {
        _isShootingSecondary = value.Get<float>() > .5f;
    }

    private void OnReload(InputValue value)
    {
        if(value.Get<float>() > .5f)
        {
            _currentWeapon?.Reload();
        }
    }

    private void OnScroll(InputValue value)
    {
        float scrollValue = value.Get<float>();
        if (scrollValue == 0) return;
        _currentWeaponIndex = (_currentWeaponIndex + (int)Mathf.Sign(scrollValue)) % _weaponList.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = _weaponList.Count - 1;
        SwitchWeapon(_currentWeaponIndex);

        _weaponList.ForEach((obj) => obj.gameObject.SetActive(false));
    }

    private void OnThrowWeapon(InputValue value)
    {
        if (value.Get<float>() < .5f) return;
        if (_weaponList.Count == 0) return;

        RemoveWeapon(_currentWeaponIndex);
    }
}


