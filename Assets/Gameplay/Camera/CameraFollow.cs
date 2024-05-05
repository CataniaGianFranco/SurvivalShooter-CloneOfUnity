using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    
    private Vector3 _offset = Vector3.zero; //Indica la separación entre el personaje y la cámara. Siempre mantedrá la misma separación inicial.
    private float _smoothing = 5.0f;

    private void Awake()
    {
        InitializeOffset();
    }

    private void FixedUpdate()
    {
        SmoothFollowTarget();
    }

    private void InitializeOffset()
    {
        _offset = transform.position - _target.position;
    }

    private void SmoothFollowTarget()
    {
        Vector3 targetCameraPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, _smoothing * Time.fixedDeltaTime);
    }
}