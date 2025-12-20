using UnityEngine;

[RequireComponent(typeof(WeaponBehavior))]
public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] private WeaponBehavior _weaponBehavior;


    private void Awake()
    {
        _weaponBehavior = GetComponent<WeaponBehavior>();
    }

    
}