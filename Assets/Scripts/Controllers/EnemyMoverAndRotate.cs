using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverAndRotate
{
    [SerializeField] private const float _lowBorderX = -5f;
    private readonly List<Enemy> _moveEnemies = new List<Enemy>();
    private readonly List<Enemy> _rotateEnemies = new List<Enemy>();

    private bool _isLevelRunning;
    
    private EventBus _eventBus;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<EnemyMoveSignal>(TryAddMove);
        _eventBus.Subscribe<EnemyRotateSignal>(TryAddRotate);
        _eventBus.Subscribe<EnemyDisposedSignal>(TryRemove);
        //_eventBus.Subscribe<SetLevelSignal>(OnLevelSet);

        //_eventBus.Subscribe<GameStartedSignal>(StartLevel);
        //_eventBus.Subscribe<GameStopSignal>(StopLevel);
    }
    
    private void TryAddMove(EnemyMoveSignal signal)
    {
        if (!_moveEnemies.Contains(signal.Value) && _isLevelRunning)
        {
            _moveEnemies.Add(signal.Value);
        }
    }
    
    private void TryAddRotate(EnemyRotateSignal signal)
    {
        if (!_rotateEnemies.Contains(signal.Value) && _isLevelRunning)
        {
            _rotateEnemies.Add(signal.Value);
        }
    }

    private void TryRemove(EnemyDisposedSignal signal)
    {
        if (_moveEnemies.Contains(signal.Value))
        {
            _moveEnemies.Remove(signal.Value);
        }
        
        if (_rotateEnemies.Contains(signal.Value))
        {
            _rotateEnemies.Remove(signal.Value);
        }
    }
    
    // private void OnLevelSet(SetLevelSignal signal)
    // {
    //     var level = signal.LevelData;
    //
    //     _startSpeed = level.StartSpeed;
    //     _endSpeed = level.EndSpeed;
    //     _levelDuration = level.LevelLength;
    // }

    // private void StartLevel(GameStartedSignal signal)
    // {
    //     _isLevelRunning = true;
    //     _timePassed = 0f;
    // }

    // private void StopLevel(GameStopSignal signal)
    // {
    //     _isLevelRunning = false;
    // }

    private void Update()
    {
        if (!_isLevelRunning)
            return;

        Move();
        Rotate();
    }

    private void LateUpdate()
    {
        if (_moveEnemies.Count == 0)
            return;

        foreach (var enemy in _moveEnemies)
        {
            if (enemy.transform.position.y <= _lowBorderX || !_isLevelRunning)
            {
                _eventBus.Invoke(new DisposeEnemySignal(enemy));
            }
        }
    }

    private void Move()
    {
        foreach (var enemy in _moveEnemies)
        {
            enemy.transform.Translate(Vector3.left * (Time.deltaTime * enemy.Speed));
        }
    }
    
    private void Rotate()
    {
        foreach (var enemy in _rotateEnemies)
        {
            enemy.transform.Rotate(Vector3.left * (Time.deltaTime * enemy.Speed));
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<EnemyMoveSignal>(TryAddMove);
        _eventBus.Unsubscribe<EnemyRotateSignal>(TryAddRotate);
        _eventBus.Unsubscribe<EnemyDisposedSignal>(TryRemove);
        //_eventBus.Unsubscribe<SetLevelSignal>(OnLevelSet);
        //_eventBus.Unsubscribe<GameStartedSignal>(StartLevel);
        //_eventBus.Unsubscribe<GameStopSignal>(StopLevel);
    }
}