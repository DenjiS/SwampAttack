using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    private Rigidbody2D _rigidbody;

    public event UnityAction<Enemy> Died;
    public event UnityAction Hitted;

    public Player Target { get; private set; }

    public int Reward => _reward;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Player target)
    {
        Target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Hitted?.Invoke();

        if (_health <= 0)
        {
            _rigidbody.bodyType = RigidbodyType2D.Static;
            Died?.Invoke(this);
        }
    }
}
