using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTicker : MonoBehaviour
{
    public UnityEvent TickEvent;

    [SerializeField] private float _defaultDelay;
    [SerializeField] private float _delayMultiplier;
    [SerializeField] private float _minDelay;
    private float _delay;

    public void SpeedUp()
    {
        if (_delay * _delayMultiplier > _minDelay) _delay *= _delayMultiplier;
    }
    public void SetSpeedToDefault() => _delay = _defaultDelay;

    private void OnEnable()
    {
        _delay = _defaultDelay;
        StartCoroutine(Tick());
    }

    private void OnDisable()
    {
        StopCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(_delay);
            TickEvent.Invoke();
        }
    }
}
