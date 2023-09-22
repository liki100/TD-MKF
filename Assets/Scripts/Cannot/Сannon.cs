using System.Collections.Generic;
using UnityEngine;

public class Сannon : MonoBehaviour
{
    [SerializeField] private СannonConfig _config;
    [SerializeField] private Projectile _projectileTemplate;
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private Transform _muzzleContainer;
    
    private float _currentCooldownTime;
    private List<Transform> _weaponMuzzles;
    private Pool<Projectile> _pool;
    private EventBus _eventBus;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DisposeProjectileSignal>(Dispose);
        _pool = new Pool<Projectile>(_projectileTemplate, 10, _projectileContainer);
        
        ResetCooldown();

        var lvl = _config.Lvl;
        var levelProjectilePoints = _config.LevelProjectilePoints;
        
        if (lvl > levelProjectilePoints.Count)
        {
            lvl = levelProjectilePoints.Count;
        }

        _weaponMuzzles = new List<Transform>();
        
        foreach (var projectilePoint in levelProjectilePoints[lvl - 1].ProjectilePoints)
        {
            _weaponMuzzles.Add(Instantiate(projectilePoint, _muzzleContainer));
        }
    }

    private void Update()
    {
        _currentCooldownTime -= Time.deltaTime;
        
        if (_currentCooldownTime > 0)
            return;

        _currentCooldownTime = 0;

        if (Input.GetKey(KeyCode.Z))
        {
            PerformedAttack();
            ResetCooldown();
        }
    }

    private void PerformedAttack()
    {
        foreach (var weaponMuzzle in _weaponMuzzles)
        {
            var projectile = _pool.Get();
            projectile.Init(_config.Damage, _config.BulletSpeed, weaponMuzzle);
        }
    }
    
    private void Dispose(DisposeProjectileSignal signal)
    {
        var projectile = signal.Value;
        _pool.Release(projectile);
    }
    
    private void ResetCooldown()
    {
        _currentCooldownTime = _config.CooldownTime;
    }
}
