using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _detectionDistance = 1.5f;
    [SerializeField] private LayerMask _detectionLayerMask;
    private Interactible _currentInteractible;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _currentInteractible = TryGetInteractible();
        CanvasManager.Instance.ShowInteractionText(_currentInteractible);
    }

    private Interactible TryGetInteractible()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(_mainCamera.pixelWidth / 2, _mainCamera.pixelHeight / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _detectionDistance, _detectionLayerMask))
        {
            Interactible interactible = hit.collider.GetComponent<Interactible>();
            return interactible;
        }
        return null;
    }

    private void OnInteract(InputValue value)
    {
        if (value.Get<float>() > .5f)
        {
            _currentInteractible?.Interact(this);
        }
    }
}

