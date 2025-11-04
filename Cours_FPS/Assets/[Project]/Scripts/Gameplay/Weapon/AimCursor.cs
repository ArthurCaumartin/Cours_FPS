using UnityEngine;

public class AimCursor : MonoBehaviour
{
    [SerializeField] private bool _debug = false;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _aimLayerMask;

    private Vector3 _worldAimPoint;

    private void Update()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _aimLayerMask))
            _worldAimPoint = hit.point;
        else
            _worldAimPoint = ray.GetPoint(10f);
    }

    public Vector3 GetWorldAimPoint()
    {
        return _worldAimPoint;
    }

    private void OnDrawGizmos()
    {
        if(!_debug) return;
        if (_playerCamera)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * 10);
            Gizmos.DrawSphere(_worldAimPoint, 0.2f);
        }
    }
}