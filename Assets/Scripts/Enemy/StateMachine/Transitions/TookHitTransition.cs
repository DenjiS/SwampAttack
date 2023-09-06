using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class TookHitTransition : Transition
{
    private Enemy _enemy;

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemy.Hitted += OnEnemyHitted;
    }

    private void OnDisable()
    {
        _enemy.Hitted -= OnEnemyHitted;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnemyHitted()
    {
        NeedTransit = true;
    }
}
