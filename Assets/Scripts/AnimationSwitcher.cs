using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSwitcher : MonoBehaviour
{
    //[SerializeField] private GameObject _externalModifier;
    [SerializeField] private GhostTrigger _ghostTrigger;

    private Animator _animator;
    private float _speed;
    //private GhostTrigger _ghostTrigger;

    //public void Awake()
    //{
    //    //_ghostTrigger = _externalModifier.GetComponent<GhostTrigger>();
        
    //}

    public void OnEnable()
    {
        _ghostTrigger.ScarySoundPlayed += OnScarySoundPlayed;
    }

    public void OnDisable()
    {
        _ghostTrigger.ScarySoundPlayed -= OnScarySoundPlayed;
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _speed = GetComponent<MovementByPoints>().Speed;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _speed);
    }
    private void OnScarySoundPlayed()
    {
        _animator.SetTrigger("Scared");
        _speed = 2f;
    }
}
