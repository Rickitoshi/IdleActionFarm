using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Backpack : MonoBehaviour
{
    [SerializeField] private float PickUpRadius;
    [SerializeField] private Transform Storage;
    [SerializeField] private Vector3 GrassBloсksStartPoint;
    [SerializeField] private float DropItemDelay;

    [Header("Count")]
    [SerializeField] private int MaxCountBlocks;
    [SerializeField] private int MaxCountX;
    [SerializeField] private int MaxCountZ;
    
    [Header("Offset between blocks")]
    [SerializeField] private float OffsetX;
    [SerializeField] private float OffsetZ;

    private Stack<GrassBlock> _grassBlocks;
    private Vector3 _grassBlockPosition;
    private int _currentCountX;
    private int _currentCountZ;

    public bool IsFull { get; private set; }
    public int CurrentCountBlocks => _grassBlocks.Count;
    public int MaxBlocks => MaxCountBlocks;

    public event Action OnDropItems;
    public event Action OnHarvestItem;
    
    private void Start()
    {
        _grassBlocks = new Stack<GrassBlock>();
        SetDefaultBlockPosition();
    }

    private void Update()
    {
        Harvest();
        IsFull = _grassBlocks.Count == MaxCountBlocks;
    }

    private void SetDefaultBlockPosition()
    {
        _grassBlockPosition = GrassBloсksStartPoint;
        _currentCountX = 1;
        _currentCountZ = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Storage.position,PickUpRadius);
    }

    public void DropItems(Vector3 position)
    {
        StartCoroutine(DropCoroutine(position));
    }
    
    private IEnumerator DropCoroutine(Vector3 position)
    {
        OnDropItems?.Invoke();
        foreach (var block in _grassBlocks)
        {
            block.Drop(position);
            yield return new WaitForSeconds(DropItemDelay);
        }
        _grassBlocks.Clear();
        SetDefaultBlockPosition();
    }
    
    private void Harvest()
    {
        Collider[] hitCollider = Physics.OverlapSphere(Storage.position,PickUpRadius);
        
        if (hitCollider.Length > 0)
        {
            foreach (var collider in hitCollider)
            {
                if (collider.GetComponent<GrassBlock>())
                {
                    GrassBlock grassBlock = collider.GetComponent<GrassBlock>();
                    
                    if (_grassBlocks.Count < MaxCountBlocks && !grassBlock.IsPickUp)
                    {
                        grassBlock.PickUp(_grassBlockPosition,Storage);
                        MovePosition();
                        OnHarvestItem?.Invoke();
                        _grassBlocks.Push(collider.GetComponent<GrassBlock>());
                    }
                }
            }
        }
    }

    private void MovePosition()
    {
        if (!MovePositionX())
        {
            if (!MovePositionY())
            {
                MovePositionZ();
            }
        }
    }
    
    private bool MovePositionX()
    {
        if (_currentCountX < MaxCountX)
        {
            _grassBlockPosition += new Vector3(OffsetX, 0, 0);
            _currentCountX++;
            return true;
        }
        
        _grassBlockPosition.x = GrassBloсksStartPoint.x;
        _currentCountX = 1;
        return false;
    }

    private bool MovePositionY()
    {
        if (_currentCountZ < MaxCountZ)
        {
            _grassBlockPosition += new Vector3(0, 0, -OffsetZ);
            _currentCountZ++;
            return true;
        }
        
        _grassBlockPosition.z = GrassBloсksStartPoint.z;
        _currentCountZ = 1;
        return false;
    }

    private void MovePositionZ()
    {
        _grassBlockPosition += new Vector3(0, 0.03f, 0);
    }
}
