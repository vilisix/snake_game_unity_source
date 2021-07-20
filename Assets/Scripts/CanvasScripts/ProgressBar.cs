using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;
    public float lerpMultiplier;
    public float coroutineTimeTick;

    public UnityEvent BarLevelReachedEvent;
    public UnityEvent BarCompletedEvent;

    private float _oneSectorValue;
    private float _currentSector;
    private float _sliceCount;
    private float _currentSlice;
    private float _targetFill;


    
    void Start()
    {
        _oneSectorValue = (float)1 / 12;
        _currentSector = 1.0f;
        _sliceCount = _currentSector * 2.0f + 1.0f;
        _currentSlice = 0.0f;
        _targetFill = (_currentSector - 1 + _currentSlice * 1 / _sliceCount) * _oneSectorValue;
        progressBar.fillAmount = _targetFill;

    }

    public void IncreaseProgress()
    {
        _currentSlice++;
        if (MoreOrEqual((_currentSector - 1 + _currentSlice * (1 / _sliceCount)),_currentSector))
        {
            _targetFill = _oneSectorValue * _currentSector;
            IncreaseDifficulty();
            BarLevelReachedEvent.Invoke();
        }
        else
        {
            _targetFill = (_currentSector - 1 + _currentSlice * 1/_sliceCount) * _oneSectorValue;
        }

        if (_targetFill >= 1.0f) BarCompletedEvent.Invoke();
        StartCoroutine(ProgressCoroutine());
    }

    public void ResetProgress()
    {
        _currentSector = 1.0f;
        _sliceCount = _currentSector * 2.0f + 1.0f;
        _targetFill = 0;
        StartCoroutine(ProgressCoroutine());
    }

    private void IncreaseDifficulty()
    {
        _currentSector++;
        _sliceCount = _currentSector * 2.0f + 1.0f;
        _currentSlice = 0.0f;
    }

    IEnumerator ProgressCoroutine()
    {
        while (progressBar.fillAmount != _targetFill)
        {
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _targetFill, lerpMultiplier);
            yield return new WaitForSeconds(coroutineTimeTick);
        }
        progressBar.fillAmount = _targetFill;
    }


    private bool MoreOrEqual(float a, float b)
    {
        if (a >= b - Mathf.Epsilon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
