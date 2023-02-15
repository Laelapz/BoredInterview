using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementHandler), typeof(HealthHandler))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Transform _boatBody;
    
    private MovementHandler _movementHandler;
    private HealthHandler _healthHandler;

    private void Awake()
    {
        _movementHandler = GetComponentInChildren<MovementHandler>();
        _healthHandler = GetComponentInChildren<HealthHandler>();
    }


    void Update()
    {
        if(!_boatBody || _healthHandler.IsDead()) return;

        if (Input.GetKey(KeyCode.W))
        {
            _movementHandler.Move();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _movementHandler.Rotate(_boatBody.right * -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _movementHandler.Rotate(_boatBody.right);
        }
    }
}
