using UnityEngine;

public class DefaultBullet : Projectile
{
    protected override void OnTargetCollision(Collision2D collision, IDamageable damageable)
    {
        damageable.ApplyDamage(Damage);
    }
}
