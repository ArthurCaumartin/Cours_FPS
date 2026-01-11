using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [SerializeField] private float _updateDelay = .5f;
    [SerializeField] private float _detectionRadius = 10;
    [SerializeField] private float _maxTrackDistance = 20;
    [SerializeField] private LayerMask _layerMask;
    private Target _currentTarget;
    private float _targetDistance;
    private float _detectionTimer;
    public Transform Target => _currentTarget?.transform;


    private void Update()
    {
        _detectionTimer += Time.deltaTime;
        if (_detectionTimer >= _updateDelay)
        {
            _detectionTimer = 0;
            DetecteTarget();
            KeepTrakOfTarget();
        }
    }

    private void DetecteTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _detectionRadius, _layerMask);
        for (int i = 0; i < cols.Length; i++)
        {
            Target t = cols[i].GetComponent<Target>();
            if (!t) continue;
            if (!_currentTarget)
            {
                _currentTarget = t;
                continue;
            }

            if (t && _currentTarget)
            {
                if (t.Priority > _currentTarget.Priority)
                    _currentTarget = t;
            }
        }
    }

    public void KeepTrakOfTarget()
    {
        if (!_currentTarget)
        {
            _targetDistance = -1;
            return;
        }

        float newDistance = (_currentTarget.transform.position - transform.position).magnitude;
        if (newDistance > _maxTrackDistance)
        {
            _currentTarget = null;
            _targetDistance = -1;
            return;
        }

        _targetDistance = newDistance;
    }

    private void OnDrawGizmos()
    {
        if (!_debug.enable) return;

        Gizmos.color = _debug.color1;
        Gizmos.DrawSphere(transform.position, _detectionRadius);
        Gizmos.color = _debug.color2;
        Gizmos.DrawSphere(transform.position, _maxTrackDistance);
    }
}
