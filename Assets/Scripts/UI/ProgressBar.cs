using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private float Durationfill;
    [SerializeField] private float DurationFade;

    private TextMeshProUGUI _text;
    private float _currentValue;
    private float _stepValue;

    public int MaxValue { get; set; }
    
    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.alpha = 0;
        _stepValue = 1f / MaxValue;
    }

    private void FixedUpdate()
    {
        if (Fill.fillAmount >= 0.99f)
        {
            _text.DOFade(1, DurationFade);
        }
    }
    
    public void IncreaseValue()
    {
        _currentValue += _stepValue;
        Fill.DOFillAmount(_currentValue, Durationfill);
    }
    
    public void ResettingValue()
    {
        _text.DOFade(0, DurationFade);
        _currentValue = 0;
        Fill.DOFillAmount(_currentValue, Durationfill);
    }
    
    
}
