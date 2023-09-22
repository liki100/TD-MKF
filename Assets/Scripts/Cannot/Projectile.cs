using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.OnAnyCollision;

    [SerializeField] private ForceMode2D _forceMode = ForceMode2D.Impulse;
    [SerializeField, Min(0f)] private float _force = 10f;
    
    [Header("Effect On Destroy")] 
    [SerializeField] private bool _spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem _effectOnDestroyTemplate;
    [SerializeField] private float _effectOnDestroyLifetime;

    private float _damage;
    public bool IsProjectileDisposed { get; private set; }
    private Rigidbody2D _rigidbody;
    protected float Damage => _damage;
    
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, float speed, Transform weaponMuzzle)
    {
        IsProjectileDisposed = false;
        _damage = damage;
        transform.position = weaponMuzzle.transform.position;
        
        _rigidbody.AddForce(weaponMuzzle.right * (_force * speed), _forceMode);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsProjectileDisposed)
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            OnTargetCollision(collision, damageable);

            if (_disposeType == ProjectileDisposeType.OnTargetCollision)
            {
                DisposeProjectile();
            }
        }
        else
        {
            OnOtherCollision(collision);
        }

        OnAnyCollision(collision);

        if (_disposeType == ProjectileDisposeType.OnAnyCollision)
        {
            DisposeProjectile();
        }
    }

    public void DisposeProjectile()
    {
        OnProjectileDispose();

        SpawnEffectOnDestroy();

        IsProjectileDisposed = true;
    }

    private void SpawnEffectOnDestroy()
    {
        if (_spawnEffectOnDestroy == false)
            return;

        var effect = Instantiate(_effectOnDestroyTemplate, transform.position, Quaternion.identity);

        Destroy(effect.gameObject, _effectOnDestroyLifetime);
    }

    protected virtual void OnProjectileDispose()
    {
        _eventBus.Invoke(new DisposeProjectileSignal(this));
    }
    protected virtual void OnAnyCollision(Collision2D collision) { }
    protected virtual void OnOtherCollision(Collision2D collision) { }
    protected virtual void OnTargetCollision(Collision2D collision, IDamageable damageable) { }
}
