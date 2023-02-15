using System.Collections;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    public int MaxLife = 1;
    public int CurrentLife = 1;

    [SerializeField] private Sprite[] sprites;

    private bool _canDamage = true;
    private bool _isDead = false;

    private BoxCollider2D _myCollider;
    private SpriteRenderer _myRenderer;

    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _myCollider = GetComponentInChildren<BoxCollider2D>();
        _myRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Hit()
    {
        if (!_canDamage) return;
        CurrentLife -= 1;
        _myRenderer.sprite = sprites[MaxLife - CurrentLife];
        //call update hud
        StartCoroutine(InvulnerabilityTime());
        if (CurrentLife <= 0) Die();
    }

    public void Die()
    {
        _myCollider.enabled = false;
        _canDamage = false;
        _isDead = true;
        _particleSystem.Play();
        StopAllCoroutines();
        StartCoroutine(DestroyAfter());
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private IEnumerator InvulnerabilityTime()
    {
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
