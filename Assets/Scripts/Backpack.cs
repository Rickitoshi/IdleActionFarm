using System;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private float PickUpRadius;
    [SerializeField] private Transform PickUpCenter;
    [SerializeField] private int MaxCount;

    private List<Transform> _grassBloks;

    private void Start()
    {
        _grassBloks = new List<Transform>();
    }

    private void FixedUpdate()
    {
        Harvest();
    }

    private void OnDrawGizmos()
    {
        if(PickUpCenter.position==Vector3.zero) return;
        Gizmos.DrawWireSphere(PickUpCenter.position,PickUpRadius);
    }

    private void Harvest()
    {
        Collider[] hitCollider = Physics.OverlapSphere(PickUpCenter.position,PickUpRadius);
        if (hitCollider.Length > 0)
        {
            foreach (var collider in hitCollider)
            {
                if (collider.GetComponent<GrassBlock>())
                {
                    GrassBlock grassBlock = collider.GetComponent<GrassBlock>();
                    if (_grassBloks.Count < MaxCount && !grassBlock.IsPickUp)
                    {
                        grassBlock.PickUp(PickUpCenter);
                        _grassBloks.Add(collider.transform);
                    }
                }
            }
        }
    }
}
