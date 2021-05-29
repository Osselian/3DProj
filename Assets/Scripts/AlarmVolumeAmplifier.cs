using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AlarmTrigger))]
public class AlarmVolumeAmplifier : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeIncreaseStrenght ;

    private AlarmTrigger _alarmTrigger;

    private void OnEnable()
    {
        _alarmTrigger = GetComponent<AlarmTrigger>();
        _audioSource.volume = 0.1f;
        _alarmTrigger.Reached += OnReached;
    }

    private void OnDisable()
    {
        _alarmTrigger.Reached -= OnReached;
    }
    
    private void OnReached()
    {
        StartCoroutine(AmplifyVolume());
    }

    private IEnumerator AmplifyVolume()
    {
        while (_audioSource.volume < 1)
        {
            _audioSource.volume += _volumeIncreaseStrenght;
            yield return null;
        }
    }

    private IEnumerator ReduceVolume()
    {
        while (_audioSource.volume > 0.01f)
        {
            _audioSource.volume -= _volumeIncreaseStrenght;
            yield return null;
        }

    }
    private void Update()
    {
        if (_audioSource.volume >= 0.9)
        {
            StopCoroutine(AmplifyVolume());
            StartCoroutine(ReduceVolume());
        }
        if (_audioSource.volume <= 0.1)
        {
            StopCoroutine(ReduceVolume());
            StartCoroutine(AmplifyVolume());
        }
    }
}
