using UnityEngine;


public class Barn : MonoBehaviour
{
    [SerializeField] private float SellRadius;
    
    private Vector3 _barPosition;

    private void Start()
    {
        _barPosition = GetComponent<Transform>().position;
    }

    private void Update()
    {
        Collider[] hitCollider = Physics.OverlapSphere(_barPosition,SellRadius);
        
        if (hitCollider.Length > 0)
        {
            foreach (var collider in hitCollider)
            {
                if (collider.GetComponent<GrassBlock>())
                {
                    GrassBlock grassBlock = collider.GetComponent<GrassBlock>();
                    if (grassBlock.IsMoveCompleted) grassBlock.Cell();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_barPosition,SellRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Backpack>())
        {
            Backpack backpack = other.GetComponent<Backpack>();
            if (backpack.CurrentCountBlocks != 0) backpack.DropItems(_barPosition);
        }
    }

}
