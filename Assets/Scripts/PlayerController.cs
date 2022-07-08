using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;

    private CharacterController _controller;
    private Animator _animator;
    private Transform _transform;
    private Joystick _joystick;
    private bool _isRun;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _animator = GetComponentInChildren<Animator>();
        _joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        PlayAnimation();
        
        if (_joystick.Direction != Vector2.zero)
        {
            Move();
            Rotate(_joystick);
        }
    }

    private void Move()
    {
        _controller.Move(transform.TransformDirection(Vector3.forward) * (MoveSpeed * Time.deltaTime));
    }
    
    private void Rotate(Joystick joystick)
    {
        Vector3 direction = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        _transform.rotation = Quaternion.Lerp(_transform.rotation,Quaternion.LookRotation(direction),RotateSpeed *Time.deltaTime);
    }

    private void PlayAnimation()
    {
        if (!_isRun && _joystick.Direction != Vector2.zero)
        {
            _animator.SetTrigger("Run");
            _isRun = true;
        }

        if (_isRun && _joystick.Direction == Vector2.zero)
        {
            _animator.SetTrigger("Idle");
            _isRun = false;
        }
    }
    
}
