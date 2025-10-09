using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected LayerMask layerMask;
    [Header("Weapon Stats : ")]
    [SerializeField] protected float damage;
    [SerializeField] protected float shootPerSecond;
    [SerializeField] protected float projectileSpeed;
    protected bool canShoot = true;

    public virtual void Shoot()
    {
        if (!canShoot) return;
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.Initilaze(damage, projectileSpeed, layerMask);

        canShoot = false;
        StartCoroutine(CanShootDelay(1f / shootPerSecond));
    }

    protected IEnumerator CanShootDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
