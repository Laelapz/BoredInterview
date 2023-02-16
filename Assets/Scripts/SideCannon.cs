using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCannon : MonoBehaviour
{
    [SerializeField] private Transform[] cannonsTransform;
    public bool isLeft = true;

    public void Shoot(string Tag)
    {
        foreach (Transform cannon in cannonsTransform)
        {
            EnemySpawner.Instance.SpawnFromPool(Tag, cannon.position, cannon.rotation);
            //Instantiate(shootPrefab, cannon.position, cannon.rotation);
        }
    }
}
