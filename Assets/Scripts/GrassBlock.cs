using System;
using DG.Tweening;
using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float JumpPower;
    [SerializeField] private float JumpDuration;
    [SerializeField] private bool JumpSnapping;
    
    [Header("Scale Settings")]
    [SerializeField] private float ScaleValue;
    [SerializeField] private float ScaleDuration;
    
    private Transform _transform;

    public bool IsPickUp { get; private set; }
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void PickUp(Transform transform)
    {
        _transform.SetParent(transform);
        _transform.DOLocalJump(Vector3.zero, JumpPower, 1, JumpDuration, JumpSnapping);
        _transform.DOScale(ScaleValue, ScaleDuration);
        IsPickUp = true;
    }
}
