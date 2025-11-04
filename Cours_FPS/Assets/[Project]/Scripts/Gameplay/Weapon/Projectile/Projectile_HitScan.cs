using UnityEngine;

public class Projectile_HitScan : Projectile
{
    private void Start()
    {
        TryGetDamagable(out Damagable damagable, out Vector3 hitPoint);
        damagable?.TakeDamage(damage);
        Debug.DrawRay(transform.position, damagable ? (hitPoint - transform.position).normalized * 100 : transform.forward * 100
                        , damagable ? Color.green : Color.red
                        , 2f);
    }

    private void TryGetDamagable(out Damagable damagable, out Vector3 hitPoint)
    {
        damagable = null;
        hitPoint = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerMask))
        {
            damagable = hit.collider.GetComponent<Damagable>();
            hitPoint = hit.point;
        }
    }
}