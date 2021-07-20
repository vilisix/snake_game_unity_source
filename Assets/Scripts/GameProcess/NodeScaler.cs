using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NodeScaler : MonoBehaviour
{
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _lerpMultiplier;
    [SerializeField] private float _lerpDelay;

    private Vector3 _startScale;

    public UnityEvent scaleDoneEvent;

    private void Awake()
    {
        _startScale = transform.localScale;
        StartCoroutine(ScaleCoroutine());
    }

    public void ScaleTo(Vector3 targetScale)
    {
        StopCoroutine(ScaleCoroutine());
        _targetScale = targetScale;
        StartCoroutine(ScaleCoroutine());
    }

    public Vector3 GetTargetScale() => _targetScale;

    public void ScaleImmediatly()
    {
        transform.localScale = _targetScale;
    }

    private void OnDestroy()
    {
        StopCoroutine(ScaleCoroutine());
    }

    IEnumerator ScaleCoroutine()    
    {
            while (transform.localScale != _targetScale)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _lerpMultiplier);
            yield return new WaitForSeconds(_lerpDelay);
        }
                transform.localScale = _targetScale;
                scaleDoneEvent.Invoke();
    }
}
