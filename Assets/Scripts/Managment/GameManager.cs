using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject GamePLayPanel;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private GameObject CoinPrefab;

    [Header("Text settings")]
    [SerializeField] private float DurationFade=1;
    [SerializeField] private float DurationScale=1;
    
    private Backpack _backpack;
    private Barn _barn;
    private CoinsCounter _coinsCounter;
    private ProgressBar _progressBar;

    private void Awake()
    {
        _backpack = FindObjectOfType<Backpack>();
        _barn = FindObjectOfType<Barn>();
        _progressBar = GamePLayPanel.GetComponentInChildren<ProgressBar>();
        _coinsCounter = GamePLayPanel.GetComponentInChildren<CoinsCounter>();

        _progressBar.MaxValue = _backpack.MaxBlocks;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        Text.alpha = 0;
        _progressBar.OnFull += ShowText;
        _backpack.OnDropItems += ResettingProgressBar;
        _backpack.OnHarvestItem += IncreaseProgressBar;
        _barn.OnCellBlock += CellBlock;
    }
    
    private void ShowText()
    {
        Text.DOFade(1, DurationFade).SetLoops(2,LoopType.Yoyo);
        Text.rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), DurationScale).SetLoops(2,LoopType.Yoyo);
    }
    
    private void IncreaseProgressBar()
    {
        _progressBar.IncreaseValue();
    }
    
    private void ResettingProgressBar()
    {
        _progressBar.ResettingValue();
    }

    private void CellBlock(int price)
    {
        GameObject coin = Instantiate(CoinPrefab, Camera.main.WorldToScreenPoint(_barn.transform.position), Quaternion.identity, _coinsCounter.transform);
        coin.transform.localScale=Vector3.zero;
        _coinsCounter.MoveCoin(coin, price);
    }

}
