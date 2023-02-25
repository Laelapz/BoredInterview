using Ship.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Ship.Units.Attack{
    public class FrontalCannon : MonoBehaviour
{
    [SerializeField] private Transform[] _cannonsTransform;
    [SerializeField] private UnityEvent _onShoot;

    public void Shoot(string Tag)
    {
        foreach (Transform cannon in _cannonsTransform)
        {
            PoolManager.Instance.SpawnFromPool(Tag, cannon.position, cannon.rotation);
            if (_onShoot != null) _onShoot.Invoke();
        }
    }
}
}
