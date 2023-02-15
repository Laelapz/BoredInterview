using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontalCannon : MonoBehaviour, IDamager
{
    [SerializeField] private Transform[] cannonsTransform;

    public void Shoot(GameObject shootPrefab)
    {
        foreach(Transform cannon in cannonsTransform)
        {
            Instantiate(shootPrefab, cannon.position, cannon.rotation);
        }
    }
}
