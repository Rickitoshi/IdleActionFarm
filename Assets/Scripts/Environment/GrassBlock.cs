using System;
using DG.Tweening;
using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    [SerializeField] private int Price = 15;
    [SerializeField] private float JumpPower;
    [SerializeField] private float JumpDuration;
    [SerializeField] private bool JumpSnapping;
    [SerializeField] private float ScaleValue;
    [SerializeField] private float ScaleDuration = 1;

    private Transform _transform;

    public bool IsPickUp { get; private set; }
    public bool IsMoveCompleted { get; private set; }
    
    //public event Action<int> OnCell; 

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void Cell()
    {
        Destroy(gameObject);
        //OnCell?.Invoke(Price);
    }
    
    public void PickUp(Vector3 position, Transform parent)
    {
        _transform.SetParent(parent);
        _transform.DOLocalJump(position, JumpPower, 1, JumpDuration, JumpSnapping);
        _transform.DOScale(ScaleValue, ScaleDuration);
        _transform.DOLocalRotate(Vector3.zero, 1,RotateMode.FastBeyond360);
        IsPickUp = true;
    }

    public void Drop(Vector3 position)
    {
        IsMoveCompleted = false;
        _transform.parent = null;
        _transform.DOMove(position, 2).OnComplete(() => { IsMoveCompleted = true; });
    }
    
}
