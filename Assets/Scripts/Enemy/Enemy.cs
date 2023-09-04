using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    public event UnityAction<Enemy> Died;

    public Player Target => _target;

    public int Reward => _reward;

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
