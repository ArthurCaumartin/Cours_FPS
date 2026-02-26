using System.Collections;
using Alchemy.Inspector;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected LayerMask layerMask;
    [Header("Weapon Stats : ")]
    [SerializeField] protected float damage;
    [SerializeField] protected float shootPerSecond;
    [SerializeField, ReadOnly] private float shootDelay;
    [SerializeField] protected float projectileSpeed;
    [Header("Recoil :")]
    [SerializeField] protected float recoilAmout;
    [SerializeField] protected float recoilDuration;
    [SerializeField] protected AnimationCurve recoilCurve;

    protected bool canShoot = true;
    protected PlayerLook playerLook;
    protected AimCursor aimCursor;
    protected WeaponInteraction weaponInteraction;
    protected WeaponControler weaponControler;
    protected Rigidbody weaponRigidbody;
    protected Collider[] weaponColliderArray;

    public float RecoilAmount => recoilAmout;
    public float RecoilDuration => recoilDuration;

    protected virtual void Awake()
    {
        enabled = false;
        weaponRigidbody = GetComponent<Rigidbody>();
        weaponColliderArray = GetComponentsInChildren<Collider>();
        weaponInteraction = GetComponent<WeaponInteraction>();
        EnableWeapon(false);
    }

    private void OnValidate()
    {
        shootDelay = 1 / shootPerSecond;
    }

    public void InitGrab(WeaponControler weaponControler, PlayerLook playerLook, AimCursor aimCursor)
    {
        if (weaponColliderArray == null)
            weaponColliderArray = GetComponentsInChildren<Collider>();

        if (weaponRigidbody)
            Destroy(weaponRigidbody);

        this.playerLook = playerLook;
        this.aimCursor = aimCursor;

        this.weaponControler = weaponControler;
        weaponInteraction.AllowInteraction = false;

        SetPhysicsState(true);
    }

    public void DropWeapon()
    {
        transform.parent = null;
        EnableWeapon(false);
        SetPhysicsState(false);
        weaponInteraction.AllowInteraction = true;
        weaponRigidbody.AddForce(transform.forward * 5f + transform.up * 2f, ForceMode.Impulse);
    }

    public virtual void EnableWeapon(bool value)
    {
        enabled = value;
    }

    public virtual void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.Initilaze(damage, projectileSpeed, layerMask);

        canShoot = false;
        StartCoroutine(CanShootDelay(1f / shootPerSecond));
    }

    public virtual void ShootSecondary(bool isInputPressed) { }

    protected IEnumerator CanShootDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    public void SetPhysicsState(bool isGrabed)
    {
        enabled = isGrabed;

        if (!isGrabed && weaponRigidbody == null)
        {
            weaponRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        foreach (var item in weaponColliderArray)
            item.enabled = !isGrabed;
    }
}


