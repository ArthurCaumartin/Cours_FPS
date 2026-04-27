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
    [SerializeField] protected float reloadDuration = 1;
    [SerializeField] protected int ammoCappacity = 5;
    [Header("Recoil :")]
    [SerializeField] protected float recoilAmout;
    [SerializeField] protected float recoilDuration;
    [SerializeField] protected AnimationCurve recoilCurve;
    [Header("FX Reference : ")]
    [SerializeField] private AudioClip _audioClipShoot;

    protected bool canShoot = true;
    protected PlayerLook playerLook;
    protected AimCursor aimCursor;
    protected WeaponVisual weaponVisual;
    protected Rigidbody weaponRigidbody;
    protected Collider[] weaponColliderArray;
    protected int currentAmmo;

    public float RecoilAmount => recoilAmout;
    public float RecoilDuration => recoilDuration;

    public bool IsMagazinEmpty => currentAmmo <= 0;

    protected virtual void Awake()
    {
        enabled = false;
        weaponRigidbody = GetComponent<Rigidbody>();
        weaponColliderArray = GetComponentsInChildren<Collider>();
        weaponVisual = GetComponent<WeaponVisual>();

        currentAmmo = ammoCappacity;

        EnableWeapon(false);
    }

    private void OnValidate()
    {
        shootDelay = 1 / shootPerSecond;
    }

    public void InitGrab(PlayerLook playerLook, AimCursor aimCursor)
    {
        if (weaponColliderArray == null)
            weaponColliderArray = GetComponentsInChildren<Collider>();

        if (weaponRigidbody)
            Destroy(weaponRigidbody);

        this.playerLook = playerLook;
        this.aimCursor = aimCursor;

        SetPhysicsState(true);
        transform.localEulerAngles = Vector3.zero;
    }

    public void DropWeapon()
    {
        transform.parent = null;
        EnableWeapon(false);
        SetPhysicsState(false);
        weaponRigidbody.AddForce(transform.forward * 5f + transform.up * 2f, ForceMode.Impulse);
    }

    public virtual void EnableWeapon(bool value)
    {
        enabled = value;
    }

    public virtual void TriggerShootFX()
    {
        AudioManager.Instance.PlayFX(_audioClipShoot, transform.position);
    }

    public virtual void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;
        if (currentAmmo <= 0) return;

        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.Initilaze(damage, projectileSpeed, layerMask);

        canShoot = false;
        StartCoroutine(CanShootDelay(1f / shootPerSecond));
        currentAmmo--;
    }

    public virtual void ShootSecondary(bool isInputPressed) { }

    public virtual void Reload()
    {
        weaponVisual.Reload(reloadDuration, () => { currentAmmo = ammoCappacity; });
    }

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


