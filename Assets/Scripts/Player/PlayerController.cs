using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private Transform DamageZoneBegin;
    [SerializeField] private Transform DamageZoneEnd;
    [SerializeField] private float DamageZoneRadius;

    private CharacterController _controller;
    private Animator _animator;
    private Transform _transform;
    private Joystick _joystick;
    private bool _isRun;
    private Collider _grass;
    private Vector3 _velocity;
    private Backpack _backpack;
    
    public bool IsAttack { get; set; }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _backpack = GetComponent<Backpack>();
        _animator = GetComponentInChildren<Animator>();
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Direction != Vector2.zero)
        {
            Move();
            Rotate(_joystick);
        }
        Gravity();
        PlayAnimation();
    }

    private void Update()
    {
        _grass = IsHitGrass();
        if (_grass!=null)
        {
            if (!_backpack.IsFull) _grass.GetComponent<Grass>().Cut();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Grass>() && !_backpack.IsFull)
        {
            if (!IsAttack) _animator.SetTrigger("Attack");
           IsAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (DamageZoneBegin.position != Vector3.zero & DamageZoneEnd.position != Vector3.zero) return;
        Gizmos.DrawSphere(DamageZoneBegin.position,DamageZoneRadius);
        Gizmos.DrawSphere(DamageZoneEnd.position,DamageZoneRadius);
    }

    private void Gravity()
    {
        _velocity.y += -9.81f * Time.deltaTime;
        _controller.Move(_velocity);
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

    private Collider IsHitGrass()
    {
        if (IsAttack)
        {
            Collider[] hitCollider = Physics.OverlapCapsule(DamageZoneBegin.position, DamageZoneEnd.position, DamageZoneRadius);
            if (hitCollider.Length > 0)
            {
                foreach (var collider in hitCollider)
                {
                    if (collider.GetComponent<Grass>())
                    {
                        return collider;
                    }
                }
            }
        }

        return null;
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
