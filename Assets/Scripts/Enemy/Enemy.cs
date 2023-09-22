using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed;

    public float Speed => _speed;

    private float _currentHealth;

    private EventBus _eventBus;

    public void Init()
    {
        _currentHealth = _health;
        
        //_eventBus.Invoke(new EnemyMoveSignal(this));
        //_eventBus.Invoke(new EnemyRotateSignal(this));
    }

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _currentHealth = _health;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * Speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpaceStation spaceStation))
        {
            Interact();
        }
    }
    
    private void Interact()
    {
        _eventBus.Invoke(new PlayerDamagedSignal(_damage));
        Dispose();
    }

    private void Dispose()
    {
        _eventBus.Invoke(new DisposeEnemySignal(this));
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _health);
        if (_currentHealth <= 0)
        {
            Dispose();
        }
    }
}
