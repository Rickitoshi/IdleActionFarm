using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private Transform _transform;
    private CoinsCounter _coinsCounter;
        
    void Start()
    {
        _transform = GetComponent<Transform>();
        _coinsCounter = FindObjectOfType<CoinsCounter>();
        _transform.DOMove(_coinsCounter.transform.position, 1);
    }
    
}
