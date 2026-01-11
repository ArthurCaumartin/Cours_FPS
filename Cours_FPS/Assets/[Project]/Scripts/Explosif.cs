using System;
using System.Reflection;
using UnityEngine;

public class Explosif : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [Space]
    [SerializeField] private float _explosionRaduis;
    [SerializeField] private float _damage;
    [Space]
    [SerializeField] private float _distanceTrigger;
    [Space]
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private LayerMask _explosionLayerMask;
    private Damagable _selfDamagable;

    private void Start()
    {
        _selfDamagable = transform.parent.GetComponent<Damagable>();
    }

    public void TryExplodeWithDistance(float distance)
    {
        if (distance < _distanceTrigger)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _explosionRaduis, _explosionLayerMask);
        for (int i = 0; i < cols.Length; i++)
        {
            Damagable d = cols[i].GetComponent<Damagable>();
            if (_selfDamagable && _selfDamagable == d) continue;
            d.TakeDamage(_damage);
        }
        ParticleSystem p = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        Destroy(p.gameObject, p.main.duration);
        Destroy(transform.parent.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!_debug.enable) return;
        Gizmos.color = _debug.color1;
        Gizmos.DrawSphere(transform.position, _distanceTrigger);
        Gizmos.color = _debug.color2;
        Gizmos.DrawSphere(transform.position, _explosionRaduis);
    }
}
