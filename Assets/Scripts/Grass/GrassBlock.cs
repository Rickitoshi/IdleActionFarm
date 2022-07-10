using DG.Tweening;
using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    [SerializeField] private float Price = 15;
    [SerializeField] private float JumpPower;
    [SerializeField] private float JumpDuration;
    [SerializeField] private bool JumpSnapping;
    [SerializeField] private float ScaleValue;
    [SerializeField] private float ScaleDuration;

    private Transform _transform;

    public float Cost => Price;

    public bool IsPickUp { get; private set; }
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void PickUp(Vector3 position, Transform parent)
    {
        _transform.SetParent(parent);
        _transform.DOLocalJump(position, JumpPower, 1, JumpDuration, JumpSnapping);
        _transform.DOScale(ScaleValue, ScaleDuration);
        _transform.DOLocalRotate(Vector3.zero, 1,RotateMode.FastBeyond360);
        IsPickUp = true;
    }
    
}
