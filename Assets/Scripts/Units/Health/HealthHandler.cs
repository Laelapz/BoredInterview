using UnityEngine;
using UnityEngine.UI;

namespace Ship.Units.Health
{
    public class HealthHandler : Damageable
    {
        private BoxCollider2D _myCollider;
        private SpriteRenderer _myRenderer;
        private Slider _slider;

        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Sprite[] _sprites;

        private void Awake()
        {
            _myCollider = GetComponentInChildren<BoxCollider2D>();
            _myRenderer = GetComponentInChildren<SpriteRenderer>();
            _slider = GetComponentInChildren<Slider>();
        }

        public void ResetStatus()
        {
            currentLife = maxLife;
            UpdateSlider();
            UpdateBoatSprite();
            isDead = false;
            CanDamage = true;
            _myCollider.enabled = true;
        }

        public void UpdateBoatSprite()
        {
            _myRenderer.sprite = _sprites[maxLife - currentLife];
        }

        public void UpdateSlider()
        {
            _slider.maxValue = maxLife;
            _slider.value = currentLife;
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}