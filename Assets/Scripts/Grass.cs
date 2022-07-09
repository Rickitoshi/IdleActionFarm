using System.Collections;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private float GrowthTime = 10;
    [SerializeField] private GameObject DropDownObject;
        
    private Transform _transformGrassUp;
    private BoxCollider _collider;
    private bool _isGrowth;

    private void Start()
    {
        _transformGrassUp = transform.Find("Grass_up");
        _collider = GetComponent<BoxCollider>();
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(GrowthTime);
        _transformGrassUp.gameObject.SetActive(true);
        _collider.enabled = true;
        _isGrowth = false;
    }

    private void Drop()
    {
        Instantiate(DropDownObject, transform.position, Quaternion.identity);
    }

    public void Cut()
    {
        if (!_isGrowth)
        {
            _collider.enabled = false;
            _transformGrassUp.gameObject.SetActive(false);
            Drop();
            StartCoroutine(Grow());
            _isGrowth = true;
        }
    }
    
}
