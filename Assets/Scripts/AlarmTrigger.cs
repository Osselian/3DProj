using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AlarmVolumeAmplifier))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();

    private AudioSource _audioSource;
    private AlarmVolumeAmplifier _alarmVolumeAmplifier;

    public event UnityAction Reached
    {
        add => _reached.AddListener(value);
        remove => _reached.RemoveListener(value);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _alarmVolumeAmplifier = GetComponent<AlarmVolumeAmplifier>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovementByPoints>(out MovementByPoints movementComponent))
        {
            _reached?.Invoke();
            _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MovementByPoints>(out MovementByPoints movementComponent)) 
        {
            _audioSource.Stop();
            _alarmVolumeAmplifier.StopAmplifier();
        }
    }
}
