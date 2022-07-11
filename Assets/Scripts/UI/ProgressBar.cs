using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private float Durationfill=1;
    [SerializeField] private float DurationFade=1;
    [SerializeField] private float DurationScale=1;
    
    private float _currentValue;
    private float _stepValue;
    private bool _isAnimationReady;

    public int MaxValue { get; set; }

    public event Action OnFull;
    
    private void Start()
    {
        //_text.alpha = 0;
        Fill.fillAmount = 0;
        _stepValue = 1f / MaxValue;
        _isAnimationReady = true;
    }

    private void Update()
    {
        if (_isAnimationReady && Fill.fillAmount >= 0.99f)
        {
            OnFull?.Invoke();
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
        Fill.DOFillAmount(_currentValue, Durationfill).OnComplete(()=>{_isAnimationReady = true;});
    }

    private void ShowText()
    {
        //_text.DOFade(1, DurationFade).SetLoops(2,LoopType.Yoyo);
        //_text.rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), DurationScale).SetLoops(2,LoopType.Yoyo);
        _isAnimationReady = false;
    }
    
}
