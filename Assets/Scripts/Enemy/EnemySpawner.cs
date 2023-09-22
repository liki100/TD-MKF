using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IService
{
    [SerializeField] private Transform _parent;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _defaultX;
    [SerializeField] private EnemyConfig _config;
    
    private readonly Dictionary<string, Pool<Enemy>> _pools =
        new Dictionary<string, Pool<Enemy>>();
    
    private EventBus _eventBus;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SpawnEnemySignal>(Spawn);
        _eventBus.Subscribe<DisposeEnemySignal>(Dispose);
    }
    
    private void Spawn(SpawnEnemySignal signal)
    {
        var enemy = _config.Get(signal.InteractableType, signal.Grade);
        var pool = GetPool(enemy);

        var item = pool.Get();
        item.transform.parent = _parent;
        item.transform.position = RandomizeSpawnPosition();

        item.Init();
    }

    private void Dispose(DisposeEnemySignal signal)
    {
        var enemy = signal.Value;
        var pool = GetPool(enemy);
        pool.Release(enemy);

        _eventBus.Invoke(new EnemyDisposedSignal(enemy));
    }
    
    private Pool<Enemy> GetPool(Enemy enemy)
    {
        var objectTypeStr = enemy.GetType().ToString();
        Pool<Enemy> pool;

        // Такого пула нет - создаём новый пул
        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new Pool<Enemy>(enemy, 5);
            _pools.Add(objectTypeStr, pool);
        }
        // Пул есть - возвращаем пул
        else
        {
            pool = _pools[objectTypeStr];
        }

        return pool;
    }
    
    private Vector3 RandomizeSpawnPosition()
    {
        var y = Random.Range(_minY, _maxY);
        return new Vector3(_defaultX, y, 0);
    }
    
    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnEnemySignal>(Spawn);
        _eventBus.Unsubscribe<DisposeEnemySignal>(Dispose);
    }
}
