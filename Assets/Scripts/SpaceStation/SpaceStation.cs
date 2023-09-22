using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private int _health = 3;

    private int _currentHealth;
    private EventBus _eventBus;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDamagedSignal>(TakeDamage);
        
        transform.position = _startPosition;

        _currentHealth = _health;
    }

    private void TakeDamage(PlayerDamagedSignal signal)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - signal.Value, 0, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
