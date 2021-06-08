using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AlarmTrigger))]
public class AlarmVolumeAmplifier : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeIncreaseStrenght ;
    [SerializeField] private float minVolume;
    [SerializeField] private float maxVolume;

    private AlarmTrigger _alarmTrigger;
    private Coroutine _amplify;
    private bool _isPlayed;

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
        _amplify = StartCoroutine(ChangeVolume(maxVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        _isPlayed = true;

        while (_isPlayed)
        {
            while (_audioSource.volume != targetVolume)
            {            
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeIncreaseStrenght);
                yield return null;        
            }
            if (targetVolume == 1)
            {
                targetVolume = 0;
            }
            else
            {
                targetVolume = 1;
            }
        }
    }

    public void StopAmplifier()
    {
        if (_amplify != null)
        {
            StopCoroutine(_amplify);
            _isPlayed = false;
        }        
    }
}
