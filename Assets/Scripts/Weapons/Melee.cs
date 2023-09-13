using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] float _radius;
    [SerializeField] int _damage;

    public override void Attack(Vector2 shootPoint, Vector2 aimPoint)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(shootPoint, _radius, Vector2.zero, 0);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit
                .collider
                .gameObject
                .TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        base.Attack(shootPoint, aimPoint);
    }
}
