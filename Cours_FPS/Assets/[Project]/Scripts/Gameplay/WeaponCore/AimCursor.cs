using UnityEngine;

public class AimCursor : MonoBehaviour
{
    [SerializeField] private bool _debug = false;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _worldAimPoint;
    private GameObject _objectAim;

    public GameObject ObjectAim => _objectAim;


    private void Update()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, 500f, _aimLayerMask);
        if (hit.collider)
        {
            _objectAim = hit.collider.gameObject;
            _worldAimPoint = hit.point;
        }
        else
        {
            _objectAim = null;
            _worldAimPoint = ray.GetPoint(100f);
        }
    }

    public Vector3 GetWorldAimPoint()
    {
        return _worldAimPoint;
    }

    private void OnDrawGizmos()
    {
        if (!_debug) return;
        if (_playerCamera)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * 500f);
            Gizmos.DrawSphere(_worldAimPoint, 0.2f);
        }
    }
}