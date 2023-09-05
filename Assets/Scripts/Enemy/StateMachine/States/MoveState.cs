using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveState : State
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
        transform.position = Vector2.MoveTowards(
            transform.position, 
            Target.transform.position, 
            _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _animator.Play(MoveCommand);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
