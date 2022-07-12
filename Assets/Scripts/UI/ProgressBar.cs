using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private float Durationfill=1;

    private float _currentValue;
    private float _stepValue;
    private bool _isFull;

    public int MaxValue { get; set; }

    public event Action OnFull;
    
    private void Start()
    {
        Fill.fillAmount = 0;
        _stepValue = 1f / MaxValue;
    }

    private void Update()
    {
        if (!_isFull && Fill.fillAmount >= 0.99f)
        {
            OnFull?.Invoke();
            _isFull = true;
        }
    }
    
    public void IncreaseValue()
    {
        _currentValue += _stepValue;
        Fill.DOFillAmount(_currentValue, Durationfill);
    }
    
    public void ResettingValue()
    {
        _currentValue = 0;
        Fill.DOFillAmount(_currentValue, Durationfill).OnComplete(()=>{_isFull = false;});
    }
}
