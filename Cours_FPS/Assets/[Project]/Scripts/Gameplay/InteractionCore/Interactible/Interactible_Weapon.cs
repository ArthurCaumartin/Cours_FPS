public class Interactible_Weapon : Interactible
{
    private WeaponBehavior _weapon;

    void Awake()
    {
        _weapon = GetComponent<WeaponBehavior>();
    }

    public override void Interact(PlayerInteract interact)
    {
        base.Interact(interact);
        WeaponControler controler = interact.GetComponentInChildren<WeaponControler>();
        controler.AddWeapon(_weapon);
    }
}