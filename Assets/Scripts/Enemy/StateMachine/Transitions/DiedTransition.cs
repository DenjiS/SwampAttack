using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DiedTransition : Transition
{
    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        NeedTransit = true;

        enemy.Died -= OnEnemyDied;
    }
}
