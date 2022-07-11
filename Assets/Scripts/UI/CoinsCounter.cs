using System;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class CoinsCounter : MonoBehaviour
{
   [SerializeField] private float SpeedUp = 0.1f;
   [SerializeField] private float ScaleDuration;
   [SerializeField] private Vector2 UpScale;
   
   private TextMeshProUGUI _textMesh;
   private RectTransform _rectTransform;
   private float _currentValue;

   private float _velosity;

   private void Start()
   {
      _rectTransform = GetComponent<RectTransform>();
      _textMesh = GetComponentInChildren<TextMeshProUGUI>();
   }

   private void Update()
   {
      if (Math.Abs(_currentValue - Convert.ToSingle(_textMesh.text)) !=0)
      {
         _textMesh.text = Math.Truncate(Mathf.Lerp(Convert.ToSingle(_textMesh.text), _currentValue, _velosity)).ToString();
         _velosity += SpeedUp * Time.deltaTime;
      }
      else
      {
         _velosity = 0;
      }
      
   }

   public void AddCoins(int value)
   {
      DOTween.Rewind(_rectTransform, false);
      _rectTransform.DOScale(new Vector3(UpScale.x, UpScale.y, 1), ScaleDuration).SetLoops(2,LoopType.Yoyo);
      _currentValue += value;
   }
   
}
