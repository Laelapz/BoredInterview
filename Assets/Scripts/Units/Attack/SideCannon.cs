using Ship.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Ship.Units.Attack
{
    public class SideCannon : MonoBehaviour
    {
        [SerializeField] private Transform[] cannonsTransform;
        [SerializeField] private UnityEvent _onShoot;

        public bool isLeft = true;

        public void Shoot(string Tag)
        {
            foreach (Transform cannon in cannonsTransform)
            {
                PoolManager.Instance.SpawnFromPool(Tag, cannon.position, cannon.rotation);
                if (_onShoot != null) _onShoot.Invoke();
            }
        }
    }
}
