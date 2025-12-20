using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private bool DEBUG = false;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _detectionOffset;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _angleThresold = 50;
    private List<Collider> _groundColliderList = new List<Collider>();
    private bool _isGrounded;
    private int _lastFrameGroundCount = 0;

    public bool IsGrounded => _groundColliderList.Count != 0;

    public delegate Collider GroundEvent();
    public event GroundEvent OnGrounded;


    private void Update()
    {
        TryDetectGroundGround();
        _isGrounded = _groundColliderList.Count != 0;

        CallOnGroundedEvent();
    }

    private void CallOnGroundedEvent()
    {
        if (_lastFrameGroundCount == 0 && _groundColliderList.Count != 0)
            OnGrounded?.Invoke();
        _lastFrameGroundCount = _groundColliderList.Count();
    }

    public void TryDetectGroundGround()
    {
        _groundColliderList.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position + Vector3.up
                                                    , _detectionRadius
                                                    , Vector3.down
                                                    , _detectionOffset
                                                    , _groundLayer);

        foreach (var item in hits)
        {
            float angle = Vector3.Angle(Vector3.up, item.normal);
            print($"Angle from UP to {item.normal} : " + angle);
            print("hit point : " + item.point);
            Debug.DrawRay(item.point, item.normal * 50, Color.blue);
            if (angle < _angleThresold)
            {
                _groundColliderList.Add(item.collider);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up - new Vector3(0, _detectionOffset, 0));
        Gizmos.DrawSphere(transform.position + Vector3.up - new Vector3(0, _detectionOffset, 0), _detectionRadius);
    }
}

