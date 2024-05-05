using UnityEngine;

public class PlayerThirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private Animator _animator = null;

    private Vector3 _movement = Vector3.zero;

    private int _floorMask = 0;
    private float _speed = 6.0f;
    private float _cameraRayLength = 100.0f;

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
    }

    private void FixedUpdate()
    {
        MapController();
        Turning();
    }

    private void MapController()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        _movement.Set(_horizontal, 0.0f, _vertical);
        
        _movement = _movement.normalized * _speed * Time.fixedDeltaTime;
        
        Animating(_horizontal, _vertical);
        
        _rigidbody.MovePosition(transform.position + _movement);
    }

    private void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(cameraRay, out floorHit, _cameraRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            _rigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float horizontal, float vertical)
    {
        bool idle = (horizontal == 0.0f) && (vertical == 0.0f);
        _animator.SetBool("IsWalking", !idle);
    }
}