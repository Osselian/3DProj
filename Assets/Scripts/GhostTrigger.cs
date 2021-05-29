 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class GhostTrigger : MonoBehaviour
{    
    [SerializeField] private Ghost[] _ghosts;
    [SerializeField] private MovementByPoints _movement;

    private UnityEvent _scarySoundPlayed = new UnityEvent();
    private AudioSource _audioSource;

    public event UnityAction ScarySoundPlayed
    {
        add => _scarySoundPlayed.AddListener(value);
        remove => _scarySoundPlayed.RemoveListener(value);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        _movement.FinalPointReached += OnPointReached;
    }
    public void OnDisable()
    {
        _movement.FinalPointReached -= OnPointReached;
    }

    public void OnPointReached()
    {
        for (int i = 0; i < _ghosts.Length; i++)
        {
            _ghosts[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            _audioSource.Play();
            _scarySoundPlayed?.Invoke();
        }
    }
}
