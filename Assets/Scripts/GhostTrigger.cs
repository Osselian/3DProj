 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostTrigger : MonoBehaviour
{    
    [SerializeField] private GameObject[] _ghosts;
    [SerializeField] private GameObject _target;

    private UnityEvent _scarySoundPlayed = new UnityEvent();

    public event UnityAction ScarySoundPlayed
    {
        add => _scarySoundPlayed.AddListener(value);
        remove => _scarySoundPlayed.RemoveListener(value);
    }

    public void OnEnable()
    {
        _target.GetComponent<MovementByPoints>().FinalPointReached += OnPointReached;
    }
    public void OnDisable()
    {
        _target.GetComponent<MovementByPoints>().FinalPointReached -= OnPointReached;
    }

    public void OnPointReached()
    {
        for (int i = 0; i < _ghosts.Length; i++)
        {
            _ghosts[i].GetComponent<MeshRenderer>().enabled = true;
            GetComponent<AudioSource>().Play();
            _scarySoundPlayed?.Invoke();
        }
    }
}
