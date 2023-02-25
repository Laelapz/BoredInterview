using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ship.Utils
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField] private UnityEvent[] onCollisionEnterActions;
        [SerializeField] private UnityEvent[] onTriggerEnterActions;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (UnityEvent action in onCollisionEnterActions)
            {
                action.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (UnityEvent action in onTriggerEnterActions)
            {
                action.Invoke();
            }
        }
    }
}
