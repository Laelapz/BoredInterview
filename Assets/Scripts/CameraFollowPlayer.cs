using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    void Update()
    {
        if (target == null) return;
        
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
