 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class GhostTrigger : MonoBehaviour
{    
    [SerializeField] private Ghost[] _ghosts;
    [SerializeField] private MovementByPoints _movement;

    private AudioSource _audioSource;

    private UnityEvent _scarySoundPlayed = new UnityEvent();    

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
            _ghosts[i].GetComponent<MeshRenderer>().enabled = true;
        }
        _audioSource.Play();
        _scarySoundPlayed?.Invoke();
    }
}
