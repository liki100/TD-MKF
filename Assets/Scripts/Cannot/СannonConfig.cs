using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonConfig", menuName = "ScriptableObjects/CannonConfig")]
public class СannonConfig : ScriptableObject
{
    [SerializeField, Min(0f)] private int _lvl = 1;
    [SerializeField, Min(0f)] private float _damage = 10f;
    [SerializeField, Range(0f,1f)] private float _bulletSpeed = 1f;
    [SerializeField] private float _defaultCooldownTime;
    [SerializeField] private List<LevelProjectilePoint> _levelProjectilePoint;

    public int Lvl => _lvl;
    public float Damage => _damage;
    public float BulletSpeed => _bulletSpeed;
    public float CooldownTime => _defaultCooldownTime;
    public List<LevelProjectilePoint> LevelProjectilePoints => _levelProjectilePoint;

    [System.Serializable]
    public class LevelProjectilePoint
    {
        [SerializeField] private List<Transform> _projectilePoints;

        public List<Transform> ProjectilePoints => _projectilePoints;
    }
}
