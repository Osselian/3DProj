using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AlarmTrigger))]
public class AlarmVolumeAmplifier : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeIncreaseStrenght ;

    private AlarmTrigger _alarmTrigger;
    private Coroutine _amplify;
    private Coroutine _reduce;

    private void Awake()
    {
        _alarmTrigger = GetComponent<AlarmTrigger>();
        _audioSource.volume = 0.1f;
    }

    private void OnEnable()
    {
        _alarmTrigger.Reached += OnReached;
    }

    private void OnDisable()
    {
        _alarmTrigger.Reached -= OnReached;
    }
    
    private void OnReached()
    {
        _amplify = StartCoroutine(AmplifyVolume());
    }

    private IEnumerator AmplifyVolume()
    {
        while (_audioSource.volume < 1)
        {
            _audioSource.volume += _volumeIncreaseStrenght;
            yield return null;
        }
        _reduce = StartCoroutine(ReduceVolume());
    }

    private IEnumerator ReduceVolume()
    {
        while (_audioSource.volume > 0.01f)
        {
            _audioSource.volume -= _volumeIncreaseStrenght;
            yield return null;
        }
        _amplify = StartCoroutine(AmplifyVolume());
    }

    public void StopAmplifier()
    {
        if (_amplify != null)        
            StopCoroutine(_amplify);

        if (_reduce != null)
            StopCoroutine(_reduce);
    }
}
