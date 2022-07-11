using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Grass : MonoBehaviour
{
    [SerializeField] private float GrowthTime = 10;
    [SerializeField] private GameObject DropDownObject;
    [SerializeField] private float ScaleDuration = 1;
        
    private Transform _transformGrassUp;
    private ParticleSystem _particle;
    private BoxCollider _collider;
    private bool _isGrowth;
    private Vector3 _defaultScale;

    private void Start()
    {
        _transformGrassUp = transform.Find("Grass_up");
        _defaultScale = _transformGrassUp.localScale;
        _collider = GetComponent<BoxCollider>();
        _particle = GetComponentInChildren<ParticleSystem>();
        _isGrowth = true;
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(GrowthTime);
        _transformGrassUp.gameObject.SetActive(true);
        _transformGrassUp.DOScale(_defaultScale, ScaleDuration).OnComplete(() => { _collider.enabled = true; });
        _isGrowth = true;
    }

    private void Drop()
    {
        Vector3 position = new Vector3(transform.position.x, 0.12f, transform.position.z);
        Instantiate(DropDownObject, position, Quaternion.identity);
    }

    public void Cut()
    {
        if (_isGrowth)
        {
            _particle.Play();
            _collider.enabled = false;
            _transformGrassUp.DOScale(0, ScaleDuration).OnComplete(() => {_transformGrassUp.gameObject.SetActive(false);});
            Drop();
            StartCoroutine(Grow());
            _isGrowth = false;
        }
    }
    
}
