using System;
using UnityEngine;

public class Explosif : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [SerializeField] private float _distanceTrigger;

    public void TryExplodeWithDistance(float distance)
    {
        if (distance < _distanceTrigger)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!_debug.enable) return;
        Gizmos.color = _debug.color1;
        Gizmos.DrawSphere(transform.position, _distanceTrigger);
    }
}
