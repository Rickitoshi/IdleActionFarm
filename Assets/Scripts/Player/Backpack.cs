using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private float PickUpRadius;
    [SerializeField] private Transform Storage;
    [SerializeField] private Vector3 GrassBloсksStartPoint;
    [SerializeField] private ProgressBar bar;

    [Header("Count")]
    [SerializeField] private int MaxCountBlocks;
    [SerializeField] private int MaxCountX;
    [SerializeField] private int MaxCountZ;
    
    [Header("Offset between blocks")]
    [SerializeField] private float OffsetX;
    [SerializeField] private float OffsetZ;

    private List<Transform> _grassBlocks;
    private Vector3 _grassBlockPosition;
    private int _currentCountX=1;
    private int _currentCountZ=1;

    public bool IsFull { get; private set; }
    
    private void Awake()
    {
        bar.MaxValue = MaxCountBlocks;
    }

    private void Start()
    {
        _grassBlocks = new List<Transform>();
        _grassBlockPosition = GrassBloсksStartPoint;
    }

    private void FixedUpdate()
    {
        Harvest();
        IsFull = _grassBlocks.Count == MaxCountBlocks;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Barn>() && _grassBlocks.Count!=0)
        {
            Vector3 position = other.GetComponent<Barn>().transform.position;
            foreach (var block in _grassBlocks)
            {
                block.parent = null;
                block.DOMove(position, 2);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Storage.position,PickUpRadius);
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
                        bar.IncreaseValue();
                        _grassBlocks.Add(collider.transform);
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
