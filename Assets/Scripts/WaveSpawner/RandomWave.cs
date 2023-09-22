using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWave : MonoBehaviour
{
    [SerializeField] private WaveConfig _config;
    [SerializeField] private float _delayFirstSpawn;
    [SerializeField] private float _spawnPositionX;
    [SerializeField] private Transform _poolContainer;

    private float _currentTime;
    private bool _isSpawned;
    private int _currentWave;
    private List<Pool<Enemy>> _pools;
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DisposeEnemySignal>(Dispose);
        _pools = new List<Pool<Enemy>>();
        
        foreach (var template in _config.EnemyTemplates)
        {
            _pools.Add(new Pool<Enemy>(template, 5, _poolContainer));
        }
        
        _currentTime = _delayFirstSpawn;
        _isSpawned = false;
        _currentWave = 0;
    }

    private void Update()
    {
        if (_isSpawned)
            return;
        
        _currentTime -= Time.deltaTime;

        if (_currentTime > 0)
            return;

        var waveTemplates = _config.Waves[_currentWave].WaveTemplates;
        Spawn(waveTemplates[RandomWaves(waveTemplates.Count)]);
        _currentTime = _config.Waves[_currentWave].Delay;
        _currentWave++;
        
        if (_currentWave >= _config.Waves.Count)
        {
            _currentWave = 0;
        }
    }

    private void Spawn(WaveTemplate wave)
    {
        foreach (var segment in wave.Segments)
        {
            StartCoroutine(SpawnEnemy(segment));
        }
    }
    
    private void Dispose(DisposeEnemySignal signal)
    {
        var item = signal.Value;
        
        foreach (var pool in _pools)
        {
            if (!pool.Has(item.name)) 
                continue;
            
            item.transform.parent = _poolContainer;
            pool.Release(item);
            break;
        }
    }

    private IEnumerator SpawnEnemy(Segment segment)
    {
        _isSpawned = true;
        var position = new Vector3(_spawnPositionX, segment.PositionY, 0);
        
        foreach (var enemies in segment.Enemies)
        {
            Pool<Enemy> currentPool = null;
            foreach (var pool in _pools)
            {
                if (!pool.Has(enemies.Enemy.name + "(Clone)")) 
                    continue;

                currentPool = pool;
                break;
            }
            
            for (var i = 0; i < enemies.Count; i++)
            {
                if (currentPool != null)
                {
                    var item = currentPool.Get();
                    item.Init();
                    item.transform.position = position;
                }
                else
                {
                    Debug.LogWarning($"{enemies.Enemy.name} is not in pool");
                }

                yield return new WaitForSeconds(1.5f);
            }
        }

        _isSpawned = false;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<DisposeEnemySignal>(Dispose);
    }

    private int RandomWaves(int waveCount)
    {
        return Random.Range(0, waveCount - 1);
    }
}

