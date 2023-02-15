using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCannon : MonoBehaviour
{
    [SerializeField] private Transform[] cannonsTransform;
    public bool isLeft = true;

    public void Shoot(GameObject shootPrefab)
    {
        foreach (Transform cannon in cannonsTransform)
        {
            Instantiate(shootPrefab, cannon.position, cannon.rotation);
        }
    }
}
