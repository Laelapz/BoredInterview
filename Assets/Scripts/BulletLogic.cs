using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float Speed = 4f;
    public float TimeBeforeExplode = 1f;

    [SerializeField] private ParticleSystem _smokeParticleSystem;
    [SerializeField] private ParticleSystem _waterParticleSystem;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _myCollider;
    private float _baseSpeed = 4f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _baseSpeed = Speed;
        _smokeParticleSystem.Play(false);
        StartCoroutine(TimerToDie());
    }

    private void OnEnable()
    {
        StartCoroutine(TimerToDie());
        _smokeParticleSystem.Play(false);
        Speed = _baseSpeed;
        _spriteRenderer.enabled = true;
        _myCollider.enabled = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Speed = _baseSpeed;
        _spriteRenderer.enabled = true;
        _myCollider.enabled = true;
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
        _myCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _waterParticleSystem.Play();
    }

    private IEnumerator TimerToDie()
    {
        yield return new WaitForSeconds(TimeBeforeExplode);
        DieEffect();
        yield return new WaitForSeconds(TimeBeforeExplode);
        gameObject.SetActive(false);
    }
}
