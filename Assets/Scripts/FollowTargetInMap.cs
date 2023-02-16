using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetInMap : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    
    void Update()
    {
        if (_target == null) return;
        
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z) + _offset;
    }
}
