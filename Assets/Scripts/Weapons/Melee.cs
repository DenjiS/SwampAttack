using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] float _radius;
    [SerializeField] int _damage;

    public override void Attack(Vector2 shootPoint, Vector2 aimPoint)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(shootPoint, _radius, Vector2.zero, 0, LayerMask.GetMask("Enemy"));

        foreach (RaycastHit2D hit in hits)
        {
            hit
                .collider
                .gameObject
                .GetComponent<Enemy>()
                .TakeDamage(_damage);
        }
    }
}
