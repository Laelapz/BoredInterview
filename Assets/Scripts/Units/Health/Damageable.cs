using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ship.Units.Health
{
    public abstract class Damageable : MonoBehaviour
    {
        public int maxLife = 1;
        protected int currentLife = 1;
        protected bool isDead = false;
        public bool CanDamage = true;
        public UnityEvent OnDead; 
        public UnityEvent OnExplode;


        [SerializeField] private UnityEvent _onDamage;

        public void Hit()
        {
            if (!CanDamage) return;
            currentLife -= 1;

            if (_onDamage != null) _onDamage.Invoke();

            StartCoroutine(InvulnerabilityTime());
            if (currentLife <= 0) Die();
        }

        public void Die()
        {
            CanDamage = false;
            isDead = true;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            StopAllCoroutines();
            StartCoroutine(DestroyAfter());
        }

        private IEnumerator DestroyAfter()
        {
            if (OnExplode != null) OnExplode.Invoke();
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
            if (OnDead != null) OnDead.Invoke();
        }

        private IEnumerator InvulnerabilityTime()
        {
            CanDamage = false;
            yield return new WaitForSeconds(0.5f);
            CanDamage = true;
        }
    }

}