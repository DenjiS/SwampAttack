using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMoveState : State
{
    private const string MoveCommand = "Move";

    [SerializeField] private float _speed;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Target == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position, 
            Target.transform.position, 
            _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _animator.Play(MoveCommand);
    }
}
