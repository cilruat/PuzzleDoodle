using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RunnerGroundCheck : MonoBehaviour
{
    public LayerMask PlatformLayerMask;

    private List<int> _colliderList = new List<int>();
    private Runner _runner = null;

    public void Awake()
    {
        _runner = GetComponentInParent<Runner>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || ((1 << collision.gameObject.layer) & PlatformLayerMask) == 0)
            return;

        if (_colliderList.Contains(collision.GetInstanceID()))
            return;
                
        _colliderList.Add(collision.GetInstanceID());

        if (_colliderList.Count == 1)
        {
            _runner.IsGround = true;            
        }            
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null || ((1 << collision.gameObject.layer) & PlatformLayerMask) == 0)
            return;

        if (!_colliderList.Contains(collision.GetInstanceID()))
            return;
                
        _colliderList.Remove(collision.GetInstanceID());

        if (_colliderList.Count == 0)
        {
            _runner.IsGround = false;
        }
    }
}
