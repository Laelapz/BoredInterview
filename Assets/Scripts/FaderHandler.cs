using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaderHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent onFadeInEnd;
    [SerializeField] private UnityEvent onFadeOutEnd;

    public void FadeInEnd()
    {
        onFadeInEnd.Invoke();
    }

    public void FadeOutEnd()
    {
        onFadeOutEnd.Invoke();
    }
}
