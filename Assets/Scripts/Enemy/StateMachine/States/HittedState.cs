using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(Animator))]
public class HittedState : State
{
    private const string HittedCommand = "Hitted";

    [SerializeField] private float _stunDuration;

    private EnemyStateMachine _stateMachine;
    private Animator _animator;

    private WaitForSeconds _delay;

    private void Awake()
    {
        _stateMachine = GetComponent<EnemyStateMachine>();
        _animator = GetComponent<Animator>();

        _delay = new WaitForSeconds(_stunDuration);
    }

    private void OnEnable()
    {
        StartCoroutine(StayingDisabled());
        _animator.Play(HittedCommand);
    }

    private IEnumerator StayingDisabled()
    {
        _stateMachine.enabled = false;
        yield return _delay;
        _stateMachine.enabled = true;
    }
}
