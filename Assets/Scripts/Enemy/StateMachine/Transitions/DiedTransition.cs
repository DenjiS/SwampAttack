using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DiedTransition : Transition
{
    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.Died += Die;
    }

    private void Die(Enemy enemy)
    {
        NeedTransit = true;

        enemy.Died -= Die;
    }
}
