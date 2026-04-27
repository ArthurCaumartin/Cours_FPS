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
    public Transform Target => _currentTarget ? _currentTarget.transform : null;


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
            Target newTarget = cols[i].GetComponent<Target>();
            if (!newTarget) continue;
            if (!_currentTarget)
            {
                _currentTarget = newTarget;
                continue;
            }

            if (newTarget && _currentTarget)
            {
                if (newTarget.Priority > _currentTarget.Priority)
                    _currentTarget = newTarget;
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

        float newDistance = Vector3.Distance(transform.position, _currentTarget.transform.position);
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
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        Gizmos.color = _debug.color2;
        Gizmos.DrawWireSphere(transform.position, _maxTrackDistance);
    }
}
