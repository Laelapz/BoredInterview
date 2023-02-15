using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementHandler), typeof(HealthHandler))]
public class EnemyIA : MonoBehaviour
{
    [SerializeField] private float _range = 1f;
    [SerializeField] private UnityEvent _attackEvent;

    private GameObject _player;
    private Transform _boatBody;
    private bool _canAttack = true;
    private MovementHandler _movementHandler;
    private HealthHandler _healthHandler;

    public float TimeReloading = 0.5f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        _movementHandler = GetComponentInChildren<MovementHandler>();
        _healthHandler = GetComponentInChildren<HealthHandler>();
    }

    private void Start()
    {
        _boatBody = transform.GetChild(0);
    }

    void Update()
    {
        if (!_boatBody || _healthHandler.IsDead()) return;
        CheckIfIsFacingPlayer();

        if (_player != null && Vector3.Distance(_boatBody.position, _player.transform.position) > _range)
        {
            _movementHandler.Move();
        }
        else
        {
            if (!_canAttack) return;
            StartCoroutine(ReloadShoot());
            _attackEvent.Invoke();
        }
    }

    private void CheckIfIsFacingPlayer()
    {
        if (!_player || !_boatBody) return;

        Vector3 dir = _boatBody.position - _player.transform.position;
        
        _movementHandler.Rotate(dir);
    }

    private IEnumerator ReloadShoot()
    {
        _canAttack = false;
        yield return new WaitForSeconds(TimeReloading);
        _canAttack = true;
    }
}
