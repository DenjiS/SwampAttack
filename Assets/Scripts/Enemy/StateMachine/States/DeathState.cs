using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    private const string DeathCommand = "Death";

    [SerializeField] private float _delay;

    private Animator _animator;
    private Collider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _animator.Play(DeathCommand);
        _collider.enabled = false;

        Destroy(gameObject, _delay);
    }
}
