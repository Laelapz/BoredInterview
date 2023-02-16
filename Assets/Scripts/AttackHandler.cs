using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private FrontalCannon _FrontalCannon;
    private SideCannon _LeftSideCannon;
    private SideCannon _RightSideCannon;

    private bool _canAttack = true;
    public float TimeReloading = 0.5f;


    private void Awake()
    {
        _FrontalCannon = GetComponent<FrontalCannon>();
        var sideCannons = GetComponents<SideCannon>();

        foreach(SideCannon side in sideCannons)
        {
            if (side.isLeft)
            {
                _LeftSideCannon = side;
            }
            else
            {
                _RightSideCannon = side;
            }
        }
    }

    public void ShootFront()
    {
        if (!_canAttack) return;

        StartCoroutine(ReloadShoot());
        _FrontalCannon.Shoot("PlayerBullet");
    }

    public void ShootLeftSide()
    {
        if (!_canAttack) return;

        StartCoroutine(ReloadShoot());
        _LeftSideCannon.Shoot("PlayerBullet");
    }

    public void ShootRightSide()
    {
        if (!_canAttack) return;

        StartCoroutine(ReloadShoot());
        _RightSideCannon.Shoot("PlayerBullet");
    }

    private IEnumerator ReloadShoot()
    {
        _canAttack = false;
        yield return new WaitForSeconds(TimeReloading);
        _canAttack = true;
    }
}
