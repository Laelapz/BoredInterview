using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float Speed = 4f;
    public float TimeBeforeExplode = 1f;

    private void Start()
    {
        StartCoroutine(TimerToDie());
    }

    void Update()
    {
        transform.Translate(-1 * Speed * Time.deltaTime * transform.up, Space.World); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthComponent = collision.gameObject.GetComponent<IDamageable>();
        if (healthComponent != null) healthComponent.Hit();
        DieEffect();
    }

    private void DieEffect()
    {
        Speed = 0f;
        GetComponent<CircleCollider2D>().enabled = false;

    }

    private IEnumerator TimerToDie()
    {
        yield return new WaitForSeconds(TimeBeforeExplode);
        DieEffect();
        Destroy(gameObject, 0.5f);
    }
}
