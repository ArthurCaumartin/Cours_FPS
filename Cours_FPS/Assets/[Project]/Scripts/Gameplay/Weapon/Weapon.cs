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

    private void OnValidate()
    {
        shootDelay = 1 / shootPerSecond;
    }

    public void SetPlayerRef(PlayerLook playerLook)
    {
        this.playerLook = playerLook;
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
}
