using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrontalCannon : MonoBehaviour, IDamager
{
    [SerializeField] private Transform[] cannonsTransform;
    [SerializeField] private UnityEvent _onShoot;

    public void Shoot(string Tag)
    {
        foreach(Transform cannon in cannonsTransform)
        {
            EnemySpawner.Instance.SpawnFromPool(Tag, cannon.position, cannon.rotation);
            if(_onShoot != null) _onShoot.Invoke();
        }
    }
}
