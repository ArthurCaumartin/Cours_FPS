using System.Collections;
using Alchemy.Inspector;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected LayerMask layerMask;
    [Header("Weapon Stats : ")]
    [SerializeField] protected float damage;
    [SerializeField] protected float shootPerSecond;
    [SerializeField, ReadOnly] private float shootDelay;
    [SerializeField] protected float projectileSpeed;
    [Space]
    [SerializeField] protected float recoilAmout;
    [SerializeField] protected float recoilDuration;

    protected bool canShoot = true;
    protected PlayerLook playerLook;
    protected WeaponControler weaponControler;
    protected Rigidbody weaponRigidbody;
    protected Collider[] weaponColliderArray;

    protected virtual void Awake()
    {
        enabled = false;
        weaponRigidbody = GetComponent<Rigidbody>();
        weaponColliderArray = GetComponentsInChildren<Collider>();
    }

    private void OnValidate()
    {
        shootDelay = 1 / shootPerSecond;
    }

    public void InitGrab(WeaponControler weaponControler, PlayerLook playerLook)
    {
        if (weaponColliderArray == null)
            weaponColliderArray = GetComponentsInChildren<Collider>();

        if (weaponRigidbody)
            Destroy(weaponRigidbody);

        this.playerLook = playerLook;
        this.weaponControler = weaponControler;

        SetPhysicsState(true);
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

        if(!isGrabed && weaponRigidbody == null)
        {
            weaponRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        foreach (var item in weaponColliderArray)
            item.enabled = !isGrabed;
    }

    public void Push(Vector3 worldForce)
    {
        weaponRigidbody.AddForce(worldForce, ForceMode.Impulse);
    }
}
