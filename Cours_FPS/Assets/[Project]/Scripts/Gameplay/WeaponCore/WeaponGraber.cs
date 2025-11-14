using UnityEngine;

public class WeaponGraber : MonoBehaviour
{
    [SerializeField] private WeaponControler _weaponControler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Weapon>(out Weapon weapon))
        {
            _weaponControler.AddWeapon(weapon);
        }
    }
}