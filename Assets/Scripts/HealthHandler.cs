using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour, IDamageable
{
    
    public int MaxLife = 1;
    public int CurrentLife = 1;
    public UnityAction onDead;

    private bool _canDamage = true;
    private bool _isDead = false;

    private BoxCollider2D _myCollider;
    private SpriteRenderer _myRenderer;
    private Slider _slider;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        _myCollider = GetComponentInChildren<BoxCollider2D>();
        _myRenderer = GetComponentInChildren<SpriteRenderer>();
        _slider = GetComponentInChildren<Slider>();
    }

    public void ResetStatus()
    {
        CurrentLife = MaxLife;
        UpdateSlider();
        UpdateBoatSprite();
        _isDead = false;
        _canDamage = true;
        _myCollider.enabled = true;
    }

    public void Hit()
    {
        if (!_canDamage) return;
        CurrentLife -= 1;

        UpdateSlider();
        UpdateBoatSprite();
        StartCoroutine(InvulnerabilityTime());
        if (CurrentLife <= 0) Die();
    }

    public void UpdateBoatSprite()
    {
        _myRenderer.sprite = sprites[MaxLife - CurrentLife];
    }

    public void UpdateSlider()
    {
        _slider.maxValue = MaxLife;
        _slider.value = CurrentLife;
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
        gameObject.SetActive(false);
        if(onDead != null) onDead.Invoke();
    }

    private IEnumerator InvulnerabilityTime()
    {
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
