using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementByPoints : MonoBehaviour
{
    [SerializeField] private Transform _pathIn;
    [SerializeField] private Transform _pathOut;
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    private UnityEvent _finalPointReached = new UnityEvent();

    public event UnityAction FinalPointReached
    {
        add => _finalPointReached.AddListener(value);
        remove => _finalPointReached.RemoveListener(value);
    }

    private Vector3[] _points;
    private bool _endPointIsReached;

    public float Speed { get; private set; }

    public void OnEnable()
    {
        GetComponentInChildren<Animator>().GetBehaviour<ScaredEnabled>().RunAway += OnRunAway;
    }

    public void OnDisable()
    {
        GetComponentInChildren<Animator>().GetBehaviour<ScaredEnabled>().RunAway -= OnRunAway;
    }

    private void Start()
    {
        SetPath(_pathIn, _speed);
        Move();
    }

    private void Update()
    {
        if (transform.position == _points[_points.Length - 1] && _endPointIsReached == false)
        {
            End();
            _endPointIsReached = true;
        }
    }
    private void Move()
    {
        Tween tween = transform.DOPath(_points, _duration / Speed, PathType.Linear).SetLookAt(0.05f);
    }

    private void SetPath(Transform path, float speed)
    {
        Speed = speed;

        _points = new Vector3[path.childCount];

        for (int i = 0; i < path.childCount; i++)
        {
            _points[i] = path.GetChild(i).position;
        }
    }

    private void OnRunAway()
    {
        SetPath(_pathOut, 2f);
        Move();
    }

    private void End()
    {
        Speed = 0;
        _finalPointReached?.Invoke();
    }
}
