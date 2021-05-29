using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _externalModifier;

    private Animator _animator;
    private float _speed;

    public void OnEnable()
    {
        _externalModifier.GetComponent<GhostTrigger>().ScarySoundPlayed += OnScared;
    }
    public void OnDisable()
    {
        _externalModifier.GetComponent<GhostTrigger>().ScarySoundPlayed -= OnScared;
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
    private void OnScared()
    {
        _animator.SetTrigger("Scared");
        _speed = 2f;
    }
}
