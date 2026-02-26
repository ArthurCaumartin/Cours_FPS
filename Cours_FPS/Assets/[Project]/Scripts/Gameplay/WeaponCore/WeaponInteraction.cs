using UnityEngine;

public class WeaponInteraction : MonoBehaviour, Interactible
{
    private WeaponBehavior _weapon;
    private bool _isInteractionAllow;
    public bool IsInteractionAllow { get => _isInteractionAllow; set => _isInteractionAllow = value; }

    private void Awake()
    {
        _weapon = GetComponent<WeaponBehavior>();
    }

    public void Interact(PlayerInteract interact)
    {
        WeaponControler weaponControler = interact.GetComponentInChildren<WeaponControler>();
        weaponControler?.AddWeapon(_weapon);
    }
}